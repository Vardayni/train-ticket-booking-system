using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace TrainTicketBookingSystem.Models
{
        public class CreateUserViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [EmailAddress]
            [Compare("Email")]
            [Display(Name = "Confirm Email")]
            public string ConfirmEmail { get; set; }

            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "User role")]
            public string SelectedRole { get; set; }

            public List<System.Web.Mvc.SelectListItem> RolesOptions { get; set; }
        }

        public class AddRoleViewModel
        {
            private List<string> currentRoles;

            [Required]
            [MinLength(3)]
            [Display(Name="Role name", Description="The name of the role")]
            public string Name { get; set; }

            public List<string> CurrentRoles
            {
                get
                {
                    this.currentRoles = this.currentRoles ?? new List<string>();
                    return this.currentRoles;
                }
                set
                {
                    this.currentRoles = value;
                }
            }
        }
}