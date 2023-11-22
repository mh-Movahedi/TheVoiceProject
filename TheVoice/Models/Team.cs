using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheVoice.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Mentor))]
        public int MentorId { get; set; }

        public Mentor Mentor { get; set; }

        public List<Candicate> Candidates { get; set; } = new();

        public double? AverageScore
        {
            get
            {
                if (Candidates != null && Candidates.Count > 0)
                {
                    var scores = Candidates.SelectMany(it => it.Scores).Where(it => it != null).ToList();

                    if (scores != null && scores.Count > 0)
                    { return scores.Average(it => it.Value); }
                }

                return null;
            }
        }
    }
}
