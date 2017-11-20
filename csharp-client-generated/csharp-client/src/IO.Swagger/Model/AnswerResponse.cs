/* 
 * DealersAndVehicles
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// AnswerResponse
    /// </summary>
    [DataContract]
    public partial class AnswerResponse :  IEquatable<AnswerResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerResponse" /> class.
        /// </summary>
        /// <param name="Success">Success.</param>
        /// <param name="Message">Message.</param>
        /// <param name="TotalMilliseconds">TotalMilliseconds.</param>
        public AnswerResponse(bool? Success = default(bool?), string Message = default(string), int? TotalMilliseconds = default(int?))
        {
            this.Success = Success;
            this.Message = Message;
            this.TotalMilliseconds = TotalMilliseconds;
        }
        
        /// <summary>
        /// Gets or Sets Success
        /// </summary>
        [DataMember(Name="success", EmitDefaultValue=false)]
        public bool? Success { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name="message", EmitDefaultValue=false)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets TotalMilliseconds
        /// </summary>
        [DataMember(Name="totalMilliseconds", EmitDefaultValue=false)]
        public int? TotalMilliseconds { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AnswerResponse {\n");
            sb.Append("  Success: ").Append(Success).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  TotalMilliseconds: ").Append(TotalMilliseconds).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as AnswerResponse);
        }

        /// <summary>
        /// Returns true if AnswerResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of AnswerResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AnswerResponse other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Success == other.Success ||
                    this.Success != null &&
                    this.Success.Equals(other.Success)
                ) && 
                (
                    this.Message == other.Message ||
                    this.Message != null &&
                    this.Message.Equals(other.Message)
                ) && 
                (
                    this.TotalMilliseconds == other.TotalMilliseconds ||
                    this.TotalMilliseconds != null &&
                    this.TotalMilliseconds.Equals(other.TotalMilliseconds)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.Success != null)
                    hash = hash * 59 + this.Success.GetHashCode();
                if (this.Message != null)
                    hash = hash * 59 + this.Message.GetHashCode();
                if (this.TotalMilliseconds != null)
                    hash = hash * 59 + this.TotalMilliseconds.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
