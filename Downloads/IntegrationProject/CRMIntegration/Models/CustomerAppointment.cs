using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMIntegration.Models
{
    public class CustomerAppointment
    {
        [Display(Name = "Preferred Days :")]
        public List<string> PreferredDays { get; set; }        

        [Display(Name = "Preferred Time :")]
        public List<string> PreferredTime { get; set; }

        [Display(Name = "Discussion Subject :")]
        public string DiscussionSubject { get; set; } // Single line text box

        [Display(Name = "Comments :")]
        public string Description { get; set; } // Multiline text box

    }

}