using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class UserDetails
    {

        [Display(Name = "First Name :")]
        public string firstName { get; set; }

        [Display(Name = "Middle Name :")]
        public string middleName { get; set; }
        [Display(Name = "Last Name :")]
        public string lastName { get; set; }
        [Display(Name = "Username :")]
        public string Username { get; set; }

        [Display(Name = "Phone No :")]
        public string mobileNumber { get; set; }

        [Display(Name = "Email Address :")]
        public string emailAddress { get; set; }

        [Display(Name = "Office Address :")]
        public string address { get; set; }

        [Display(Name = "companyId :")]
        public string companyId { get; set; }

        [Display(Name = "Company Name :")]
        public string companyName { get; set; }

        [Display(Name = "Password :")]
        public string password { get; set; }
    }
}