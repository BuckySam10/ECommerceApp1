using Microsoft.AspNetCore.Authorization;

namespace ECommerceApp.Authorization
{
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public AdminOnlyAttribute()
        {
            Roles = "Admin";
        }
    }

    public class UserOrAdminAttribute : AuthorizeAttribute
    {
        public UserOrAdminAttribute()
        {
            Roles = "User,Admin";
        }
    }
}