using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TheVoice.Data;
using TheVoice.Models;

namespace TheVoice.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public SeedController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            if (_context != null)
            {
                if (_context.Users.Count() == 0)
                {
                    var adminUser = AddIdentityUser("admin1", "a1@X.c", "123");
                    var mentorUser1 = AddIdentityUser("mentor1", "m1@X.c", "123");
                    var mentorUser2 = AddIdentityUser("mentor2", "m2@X.c", "123");
                    var candidUser1 = AddIdentityUser("candid1", "c1@X.c", "123");
                    var candidUser2 = AddIdentityUser("candid2", "c2@X.c", "123");

                    IdentityRole adminR = AddRole("admin");
                    IdentityRole mentorR = AddRole("mentor");
                    IdentityRole candidR = AddRole("candid");

                    AddRoleToUser(adminUser, adminR);
                    AddRoleToUser(mentorUser1, mentorR);
                    AddRoleToUser(mentorUser2, mentorR);
                    AddRoleToUser(candidUser1, candidR);
                    AddRoleToUser(candidUser2, candidR);

                    Mentor mentor1 = new Mentor() { Name = "Mentor1", UserId = mentorUser1.Id };
                    Mentor mentor2 = new Mentor() { Name = "Mentor2", UserId = mentorUser2.Id };

                    _context.Mentores.AddRangeAsync(new List<Mentor>() { mentor1, mentor2 });
                    _context.SaveChanges();

                    Team team1 = new Team() { Name = "Team1", MentorId = mentor1.Id };
                    Team team2 = new Team() { Name = "Team2", MentorId = mentor2.Id };

                    _context.Teams.AddRangeAsync(new List<Team>() { team1, team2 });
                    _context.SaveChanges();

                    Candicate candicate1 = new Candicate() { Name = "Candid1", TeamId = team1.Id, UserId = candidUser1.Id };
                    Candicate candicate2 = new Candicate() { Name = "Candid2", TeamId = team2.Id, UserId = candidUser2.Id };

                    _context.Candicates.AddRangeAsync(new List<Candicate>() { candicate1, candicate2 });
                    _context.SaveChanges();

                    Activity activity = new Activity() { Date = DateTime.Now, SongName = "Song1" };

                    _context.Activities.AddAsync(activity);
                    _context.SaveChanges();

                    Score score1 = new Score() { ActivityId = activity.Id, CandicateId = candicate1.Id, MentorId = mentor1.Id, Value = 28 };
                    Score score2 = new Score() { ActivityId = activity.Id, CandicateId = candicate2.Id, MentorId = mentor1.Id, Value = 30 };
                    Score score3 = new Score() { ActivityId = activity.Id, CandicateId = candicate1.Id, MentorId = mentor2.Id, Value = 70 };
                    Score score4 = new Score() { ActivityId = activity.Id, CandicateId = candicate2.Id, MentorId = mentor2.Id, Value = 80 };

                    _context.Scores.AddRangeAsync(new List<Score>() { score1, score2, score3, score4 });
                    _context.SaveChanges();
                    return View("index","Done");
                }
                else
                { return View("index", "Already filled"); }
            }
            else
            {
                return View("index", "No Context");
            }
        }

        private IdentityRole AddRole(string name)
        {
            var role = new IdentityRole() { Name = name, NormalizedName = name.ToUpper() };
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Task<IdentityResult>? AddRoleToUser(IdentityUser user, IdentityRole role)
        {
            Task<IdentityResult>? result = _userManager.AddToRoleAsync(user, role.Name);
            _context.SaveChanges();
            return result;
        }

        private IdentityUser AddIdentityUser(string name, string email, string password)
        {
            IdentityUser applicationUser = new IdentityUser();
            Guid guid = Guid.NewGuid();
            applicationUser.Id = guid.ToString();
            applicationUser.UserName = name;
            applicationUser.Email = email;
            applicationUser.NormalizedUserName = email.ToUpper();
            applicationUser.EmailConfirmed = true;

            _context.Users.Add(applicationUser);


            var hashedPassword = _passwordHasher.HashPassword(applicationUser, password);
            applicationUser.SecurityStamp = Guid.NewGuid().ToString();
            applicationUser.PasswordHash = hashedPassword;

            _context.SaveChanges();

            return applicationUser;
        }
    }
}
