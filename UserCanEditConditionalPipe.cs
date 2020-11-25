namespace ConditionalPipe
{
    public class UserCanEditConditionalPipe : IConditionalPipe
    {
        private readonly User _user;
        public UserCanEditConditionalPipe(User user)
        {
            _user = user;
        }

        public bool Check()
        {
            return _user.Permission.HasFlag(UserPermission.Write);
        }
    }
}
