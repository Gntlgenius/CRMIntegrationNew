using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class contactEntity
    {
        [JsonProperty("firstname")]
        public string firstName { get; set; }

        [JsonProperty("middlename")]
        public string middleName { get; set; }

        [JsonProperty("lastname")]
        public string lastName { get; set; }

        [JsonProperty("mobilephone")]
        public string mobileNumber { get; set; }

        [JsonProperty("emailaddress1")]
        public string emailAddress { get; set; }

        [JsonProperty("_parentcustomerid_value")]
        public string companyId { get; set; }

        [JsonProperty("_parentcustomerid_value@OData.Community.Display.V1.FormattedValue")]
        public string companyName { get; set; }

    }
}