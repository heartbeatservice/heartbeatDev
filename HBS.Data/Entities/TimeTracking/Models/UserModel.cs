using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(MembershipUserExtended mue)
            :this()
        {
            UserName = mue.UserName;
            Email = mue.Email;
            FirstName = mue.FirstName;
            LastName = mue.LastName;
            Title = mue.Title;
            HourlyRate = mue.HourlyRate;
            Phone = mue.Phone;
            Address = mue.Address;
            City = mue.City;
            State = mue.State;
            Zip = mue.Zip;
            Password = mue.Password;
            LastActivityDate = mue.LastActivityDate;
            IsOnline = mue.IsOnline;
            UserRoles = mue.UserRoles;
        }

        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public virtual string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Email address")]
        public virtual string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Hourly Rate")]
        public double? HourlyRate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string State { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Activity Date")]
        public DateTime LastActivityDate{ get; set; }

        [Display(Name = "Is Online")]
        public bool IsOnline{ get; set; }

        public List<string> UserRoles { get; set; }
    }

    public sealed class CreateUserModel : UserModel
    {
        

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public override string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Email address")]
        public override string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public CreateUserModel()
        {
        }

        public CreateUserModel(MembershipUserExtended mue)
        {
            UserName = mue.UserName;
            Email = mue.Email;
            FirstName = mue.FirstName;
            LastName = mue.LastName;
            Title = mue.Title;
            HourlyRate = mue.HourlyRate;
            Phone = mue.Phone;
            Address = mue.Address;
            City = mue.City;
            State = mue.State;
            Zip = mue.Zip;
            LastActivityDate = mue.LastActivityDate;
            IsOnline = mue.IsOnline;
            UserRoles = mue.UserRoles;
        }
    }
}