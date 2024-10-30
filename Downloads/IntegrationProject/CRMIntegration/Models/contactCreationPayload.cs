using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CRMIntegration.Models
{
    public class contactCreationPayload
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

        [JsonProperty("address1_composite")]
        public string address { get; set; }


    }
}