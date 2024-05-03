


namespace LMS.WebFrontend.Models.ViewModels
{
    public class MemberVM
    {
        
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
