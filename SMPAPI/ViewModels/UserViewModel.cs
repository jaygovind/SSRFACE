using System.ComponentModel.DataAnnotations;

namespace SMPAPI.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        //[DataType(DataType.Password)]
        public string Password { get; set; }
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }


        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }


        public string OrganisationName { get; set; }
    }
}
