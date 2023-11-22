using Microsoft.AspNetCore.Identity;

namespace TheVoice.Providers.User
{
    public class UserDataProvider
    {
        public static async Task<IdentityUser> Get(UserManager<IdentityUser> userManager, HttpContext httpContext)
        {
            return await userManager.GetUserAsync(httpContext.User);
        }
    }
}
