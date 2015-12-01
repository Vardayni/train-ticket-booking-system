using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainTicketBookingSystem.ViewModels.Manage
{
    public class AccountSettingsViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public bool HasPassword { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}