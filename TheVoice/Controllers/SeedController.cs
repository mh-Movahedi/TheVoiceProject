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
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public SeedController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IPasswordHasher<IdentityUser> passwordHasher
            ,RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index()
        {
            if (_context != null)
            {
                if (_context.Users.Count() == 0)
                {
                    var adminUser=await CreateUserAndRole("admin", "Admin", "a@x.c", "12345678");
                    var mentorUser1 = await CreateUserAndRole("mentor", "mentor1", "m1@x.c", "12345678");
                    var mentorUser2 = await CreateUserAndRole("mentor", "mentor2", "m2@x.c", "12345678");
                    var candidUser1 = await CreateUserAndRole("candid", "candid1", "c1@x.c", "12345678");
                    var candidUser2 = await CreateUserAndRole("candid", "candid2", "c2@x.c", "12345678");
                    var candidUser3 = await CreateUserAndRole("candid", "candid3", "c2@x.c", "12345678");

                    if (adminUser==null || mentorUser1==null|| mentorUser2==null || candidUser1==null || candidUser2== null || candidUser3 == null)
                    { return View("index", "Failed to create a user"); }

                    Mentor mentor1 = new Mentor() { Name = "Mentor1", UserId = mentorUser1.Id };
                    Mentor mentor2 = new Mentor() { Name = "Mentor2", UserId = mentorUser2.Id };

                     await _context.Mentores.AddRangeAsync(new List<Mentor>() { mentor1, mentor2 });
                    _context.SaveChanges();

                    Team team1 = new Team() { Name = "Team1", MentorId = mentor1.Id };
                    Team team2 = new Team() { Name = "Team2", MentorId = mentor2.Id };

                     await _context.Teams.AddRangeAsync(new List<Team>() { team1, team2 });
                    _context.SaveChanges();

                    Activity activity1 = new Activity() { Date = DateTime.Now, SongName = "Song1" };
                    Activity activity2 = new Activity() { Date = DateTime.Now.AddDays(-1), SongName = "Song2" };

                    await _context.Activities.AddRangeAsync(new List<Activity>() { activity1, activity2 });
                    _context.SaveChanges();


                    Candicate candicate1 = new Candicate() { Name = "Candid1", TeamId = team1.Id, UserId = candidUser1.Id, Activities = new() { activity1, activity2 } };
                    Candicate candicate2 = new Candicate() { Name = "Candid2", TeamId = team2.Id, UserId = candidUser2.Id, Activities = new() { activity1 } };
                    Candicate candicate3= new Candicate() { Name = "Candid3", TeamId = team1.Id, UserId = candidUser3.Id, Activities = new() { activity1, activity2 } };

                    await _context.Candicates.AddRangeAsync(new List<Candicate>() { candicate1, candicate2, candicate3 });
                    _context.SaveChanges();


                    Score score1 = new Score() { ActivityId = activity1.Id, CandicateId = candicate1.Id, MentorId = mentor1.Id, Value = 28 };
                    Score score2 = new Score() { ActivityId = activity1.Id, CandicateId = candicate2.Id, MentorId = mentor1.Id, Value = 30 };
                    Score score3 = new Score() { ActivityId = activity1.Id, CandicateId = candicate1.Id, MentorId = mentor2.Id, Value = 70 };
                    Score score4 = new Score() { ActivityId = activity1.Id, CandicateId = candicate2.Id, MentorId = mentor2.Id, Value = 80 };
                    Score score5 = new Score() { ActivityId = activity2.Id, CandicateId = candicate1.Id, MentorId = mentor1.Id, Value = 68 };
                    Score score6 = new Score() { ActivityId = activity2.Id, CandicateId = candicate1.Id, MentorId = mentor2.Id, Value = 60 };

                    Score score7 = new Score() { ActivityId = activity1.Id, CandicateId = candicate3.Id, MentorId = mentor1.Id, Value = 68 };
                    Score score8 = new Score() { ActivityId = activity1.Id, CandicateId = candicate3.Id, MentorId = mentor2.Id, Value = 78 };
                    Score score9 = new Score() { ActivityId = activity2.Id, CandicateId = candicate3.Id, MentorId = mentor1.Id, Value = 88 };
                    Score score10 = new Score() { ActivityId = activity2.Id, CandicateId = candicate3.Id, MentorId = mentor2.Id, Value = 90 };

                    await _context.Scores.AddRangeAsync(new List<Score>() { score1, score2, score3, score4, score5, score6, score7, score8, score9, score10 });
                    _context.SaveChanges();
                    
                    return View("index", "Done");
                }
                else
                { return View("index", "Already filled"); }
            }
            else
            {
                return View("index", "No Context");
            }
        }

        private async Task<IdentityRole> AddRole(string name)
        {
            IdentityRole role = new IdentityRole(name);
             _ = await _roleManager.CreateAsync(role);
            return role;
        }

        public Task<IdentityResult> AddRoleToUser(IdentityUser user, IdentityRole role)
        {
            Task<IdentityResult> result = _userManager.AddToRoleAsync(user, role.Name);
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

        private async Task<IdentityUser?> CreateUserAndRole(string roleName, string name, string email, string password)
        {
            
            var roleExists =await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
            }

            IdentityUser applicationUser = new IdentityUser();
            Guid guid = Guid.NewGuid();
            applicationUser.Id = guid.ToString();
            applicationUser.UserName = name;
            applicationUser.Email = email;
            applicationUser.NormalizedUserName = email.ToUpper();
            applicationUser.EmailConfirmed = true;

            var result= await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, roleName);
                return applicationUser;
            }
            return null;
        }
    }
}
