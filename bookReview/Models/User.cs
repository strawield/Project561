using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace bookReview.Models
{
    public class User
    {   [Key]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required ] 
        public string UserName { get; set; }
        
        public DateTime BirthDate { get; set; }
        [Required]
        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 1,
        MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
        ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
        MinRequiredPasswordLength = 6
        )]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [EmailAddress(ErrorMessage ="Enter Valid eMail")]
        public string eMail { get; set; }
        public bool Admin { get; set; }
    }
}