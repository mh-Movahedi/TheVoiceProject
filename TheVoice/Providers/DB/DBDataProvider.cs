using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheVoice.Data;
using TheVoice.Models;
using TheVoice.Providers.User;

namespace TheVoice.Providers.DB
{
    public class DBDataProvider
    {
        public static async Task<Mentor?> GetMentorFullDataForUser(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            HttpContext httpContext)
        {
            //its better to only get properties we need but to be brife we get all here
            if (context.Mentores != null)
            {
                var user = await UserDataProvider.Get(userManager, httpContext);
                var mentor = await context.Mentores
                    .Include(it => it.Activities)
                    .Include(it => it.Teams)
                    .ThenInclude(it => it.Candidates)
                    .ThenInclude(it => it.Scores)
                    .FirstOrDefaultAsync();// (it => it.UserId == user.Id);//TODO uncomment & replace

                if (mentor != null)//user != null && //TODO uncomment & add
                { return mentor; }
            }

            return null;
        }
    }
}
