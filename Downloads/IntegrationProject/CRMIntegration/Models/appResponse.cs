using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class appResponse
    {
        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("@odata.count")]
        public int OdataCount { get; set; }

        [JsonProperty("value")]
        public IEnumerable<AppointmentReqEntity> Value { get; set; } = new List<AppointmentReqEntity>();
    }

}