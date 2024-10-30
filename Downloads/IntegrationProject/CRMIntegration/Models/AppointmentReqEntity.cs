using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class AppointmentReqEntity
    {
        [JsonProperty("statuscode@OData.Community.Display.V1.FormattedValue")]
        public string status { get; set; } 

        [JsonProperty("sm_preferreddays@OData.Community.Display.V1.FormattedValue")]
        public string PreferredDays { get; set; } 

        [JsonProperty("sm_preferredtime@OData.Community.Display.V1.FormattedValue")]
        public string PreferredTime { get; set; } 

        [JsonProperty("sm_name")]
        public string DiscussionSubject { get; set; } 

        [JsonProperty("sm_description")]
        public string Description { get; set; }

        [JsonProperty("createdon")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("sm_customer@odata.bind")]
        public string contactid { get; set; }

    }



    public class AppointmentCreationReqEntity
    {
        // Using a List<string> to represent multiple preferred days
        [JsonProperty("sm_preferreddays")]
        public string PreferredDays { get; set; }

        // Using a List<string> to represent multiple preferred times
        [JsonProperty("sm_preferredtime")]
        public string PreferredTime { get; set; }

        [JsonProperty("sm_name")]
        public string DiscussionSubject { get; set; }

        [JsonProperty("sm_description")]
        public string Description { get; set; }

        [JsonProperty("sm_Customer@odata.bind")]
        public string contactId { get; set; } // Assuming contactid is of type Guid
    }
}