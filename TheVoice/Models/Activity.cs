using System.ComponentModel.DataAnnotations;

namespace TheVoice.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        public string SongName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public List<Score> Scores { get; set; } = new();

        public double? AverageScore {
            get => Scores != null && Scores.Count > 0 ? Scores.Average(it => it.Value) : null;
        }

        public List<Mentor> Mentors { get; set; } = new();

        public List<Candicate> Candicates { get; set; } = new();
    }
}
