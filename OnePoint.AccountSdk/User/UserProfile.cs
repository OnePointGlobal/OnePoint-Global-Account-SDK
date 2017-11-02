namespace OnePoint.AccountSdk.User
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Gmt { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public double Credit { get; set; }
        public string SharedKey { get; set; }
    }

    public class RootObject
    {
        public UserProfile UserProfile { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
