using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheVoice.Models
{
    public class Candicate
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Team))]
        public int? TeamId { get; set; }

        public Team? Team { get; set; }

        public List<Activity> Activities { get; set; } = new();

        public List<Score> Scores { get; set; } = new();
    }
}
