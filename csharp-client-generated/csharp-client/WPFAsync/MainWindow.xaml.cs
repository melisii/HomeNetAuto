using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;

using IO.Swagger.Client;
using IO.Swagger.Api;
using IO.Swagger.Model;

namespace WPFAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataSetApi dsInstance;
        VehiclesApi vehicleInstance;
        DealersApi dealerInstance;

        List<VehicleResponse> vehicleList = new List<VehicleResponse>();
        List<DealersResponse> dealerList = new List<DealersResponse>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            resultsTextBox.Clear();
            vehicleList.Clear();
            dealerList.Clear();

            dsInstance = new DataSetApi();
            vehicleInstance = new VehiclesApi();
            dealerInstance = new DealersApi();

            Answer answer = new Answer();

            // Get DataSet ID
            string datasetId = "";
            DatasetIdResponse responseDataSetId = dsInstance.DataSetGetDataSetId();
            datasetId = responseDataSetId.DatasetId;

            // Get the Dealers and Vehicles for the given DataSetID

            // Get VehicleIds
            VehicleIdsResponse responseVehicleIds = new VehicleIdsResponse();
            responseVehicleIds = vehicleInstance.VehiclesGetIds(datasetId);

            //Get Vechicle details for each VehicleId
            VehicleResponse responseVehicle = new VehicleResponse();

            await CreateVehicleTasksAsync(datasetId, responseVehicleIds);

            // For the vehicles, get the dealers
            var Ids = vehicleList.Select(v => (int)v.DealerId).ToList().Distinct();
            List<int> dealerIds = Ids.ToList();

            await CreateDealerTasksAsync(datasetId, dealerIds);
          
            // Once Vehicle list and Dealer list are done, build the answer
            List<DealerAnswer> dealerAnswers = new List<DealerAnswer>();

            foreach (DealersResponse dealer in dealerList)
            {
                DealerAnswer dealerAnswer = new DealerAnswer();
                dealerAnswer.DealerId = dealer.DealerId;
                dealerAnswer.Name = dealer.Name;

                List<VehicleAnswer> dealerVehicles = new List<VehicleAnswer>();
                List<VehicleResponse> vehicles = vehicleList.Where(v => v.DealerId == dealer.DealerId).ToList();

                foreach (VehicleResponse res in vehicles)
                {
                    VehicleAnswer vehicle = new VehicleAnswer();
                    vehicle.VehicleId = res.VehicleId;
                    vehicle.Make = res.Make;
                    vehicle.Model = res.Model;
                    vehicle.Year = res.Year;
                    dealerVehicles.Add(vehicle);
                }

                dealerAnswer.Vehicles = dealerVehicles;
                dealerAnswers.Add(dealerAnswer);
            }

            answer.Dealers = dealerAnswers;

            // Post the response answer
            AnswerResponse ansResponse = dsInstance.DataSetPostAnswer(datasetId, answer);

            string results = ansResponse.Message + "\r\n\r\n" + ansResponse.Success + "\r\n\r\nTime(seconds): " + ansResponse.TotalMilliseconds/1000.00;

            foreach(DealerAnswer d in answer.Dealers)
            {
                results += "\r\n\r\n----------------------------------------------------------------\r\n\r\n" + d.Name;
                results += "\r\n\r\n-------------------------";
                foreach ( VehicleAnswer v in d.Vehicles)
                {
                    //results += "\r\n\r\n";
                    results += "\r\n\r\n" + v.Make + "  " + v.Model + " " + v.Year;
                }
            }
            resultsTextBox.Text = results;
        }


        async Task<VehicleResponse> ProcessURLVehicleAsync(string datasetId, int vehicleId)
        {
            vehicleInstance = new VehiclesApi();
            var response = await vehicleInstance.VehiclesGetVehicleAsync(datasetId, vehicleId);
            return response;
        }


        private async Task CreateVehicleTasksAsync(string datasetId, VehicleIdsResponse responseVehicleIds)
        {
            List<Task<VehicleResponse>> vTaskList = new List<Task<VehicleResponse>>();

            foreach(int id in responseVehicleIds.VehicleIds)
            {
                Task<VehicleResponse> vResponse = ProcessURLVehicleAsync(datasetId, (int)id);
                vTaskList.Add(vResponse);
            }

            // Await each task. 
            for (int i = 0; i< vTaskList.Count; i++)
            {
                VehicleResponse vr = await vTaskList[i];
                vehicleList.Add(vr);
            }
        }


        async Task<DealersResponse> ProcessURLDealerAsync(string datasetId, int dealerId)
        {
            dealerInstance = new DealersApi();
            var response = await dealerInstance.DealersGetDealerAsync(datasetId, dealerId);
            return response;
        }


        private async Task CreateDealerTasksAsync(string datasetId, List<int> dealerIds)
        {
            List<Task<DealersResponse>> dTaskList = new List<Task<DealersResponse>>();

            foreach (int id in dealerIds)
            {
                Task<DealersResponse> vResponse = ProcessURLDealerAsync(datasetId, id);
                dTaskList.Add(vResponse);
            }

            // Await each task.  
            for (int i = 0; i < dTaskList.Count; i++)
            {
                DealersResponse dr = await dTaskList[i];
                dealerList.Add(dr);
            }
        }


    }

}

