namespace ConditionalPipe
{
    public class UserIsAdminConditionalPipe : IConditionalPipe
    {
        private readonly User _user;
        public UserIsAdminConditionalPipe(User user)
        {
            _user = user;
        }

        public bool Check()
        {
            return _user.Role == UserRole.Admin;
        }
    }
}
