using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class CreateUser
    {
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string middleName { get; set; }
        public string emailAddress { get; set; }
        public string companyName { get; set; }
        public string address { get; set; }
        public string companyId { get; set; }	
        public string contactId { get; set; }

    }
}