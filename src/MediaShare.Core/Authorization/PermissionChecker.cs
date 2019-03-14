using Abp.Authorization;
using MediaShare.Authorization.Roles;
using MediaShare.Authorization.Users;

namespace MediaShare.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
