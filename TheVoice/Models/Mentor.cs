using System.ComponentModel.DataAnnotations;

namespace TheVoice.Models
{
    public class Mentor
    {
        [Key]
        public int Id { get; set; }

        public List<Team> Teams { get; set; } = new();

        public List<Activity> Activities { get; set; } = new();

        public List<Score> Scores { get; set; } = new();
    }
}
