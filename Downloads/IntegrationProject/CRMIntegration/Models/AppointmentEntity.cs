using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class AppointmentEntity
    {
        public string executiveName { get; set; }
        public string status { get; set; }
        public string subject { get; set; }
        public string relattionshipMgr { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}