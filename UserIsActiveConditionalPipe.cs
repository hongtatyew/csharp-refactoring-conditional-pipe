namespace ConditionalPipe
{
    public class UserIsActiveConditionalPipe : IConditionalPipe
    {
        private readonly User _user;
        public UserIsActiveConditionalPipe(User user)
        {
            _user = user;
        }

        public bool Check()
        {
            return _user.Status == UserStatus.Active;
        }
    }
}
