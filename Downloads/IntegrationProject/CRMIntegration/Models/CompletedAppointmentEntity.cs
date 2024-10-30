using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{

    public class compAppResponse
    {
        [JsonProperty("value")]
        public IEnumerable<CompletedAppointmentEntity> Value { get; set; } = new List<CompletedAppointmentEntity>();
    }

    public class CompletedAppointmentEntity
    {
        [JsonProperty("subject")]
        public string subject { get; set; }

        [JsonProperty("statuscode@OData.Community.Display.V1.FormattedValue")]
        public string Status { get; set; }        

        [JsonProperty("scheduledstart")]
        public DateTime startTime { get; set; }

        [JsonProperty("scheduledend")]
        public DateTime endTime { get; set; }

        [JsonProperty("regardingobjectid_sm_appointmentrequest_appointment")]
        public AppreqClass relatedAppReq { get; set; }

    }

    public class AppreqClass
    {
        [JsonProperty("_sm_customer_value@OData.Community.Display.V1.FormattedValue")]
        public string ExecutiveName { get; set; }

        [JsonProperty("_sm_relationshipmanager_value@OData.Community.Display.V1.FormattedValue")]
        public string relMgrName { get; set; }
    }

    public class CompletedAppointmentsDto
    {
        public string subject { get; set; }
        public string status { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string executiveName { get; set; }
        public string relMgerName { get; set; }

    }

}