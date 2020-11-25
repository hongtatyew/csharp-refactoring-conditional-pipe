namespace ConditionalPipe
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }
        public string Role { get; set; }
        public UserPermission Permission { get; set; }
    }
}