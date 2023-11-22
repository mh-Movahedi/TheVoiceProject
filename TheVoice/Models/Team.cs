using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheVoice.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Mentor))]
        public int MentorId { get; set; }

        public Mentor Mentor { get; set; }

        public List<Candicate> Candidates { get; set; } = new();
    }
}
