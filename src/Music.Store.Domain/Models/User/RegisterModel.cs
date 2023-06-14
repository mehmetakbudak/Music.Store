namespace Music.Store.Domain.Models.User
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Phone { get; set; }
    }
}
