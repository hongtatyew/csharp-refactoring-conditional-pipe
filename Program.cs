using System;

namespace ConditionalPipe
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentUser = new User()
            {
                FirstName = "Izuku",
                LastName = "Midoriya",
                Status = UserStatus.Active,
                Role = UserRole.Admin,
                Permission = UserPermission.Read | UserPermission.Write
            };

            var activeAdminCanEdit = ConditionalPipes.Check(
                new UserIsAdminConditionalPipe(currentUser),
                new UserIsActiveConditionalPipe(currentUser),
                new UserCanEditConditionalPipe(currentUser)
            );

            if (activeAdminCanEdit)
            {
                // ... some implementation
            }
        }
    }
}
