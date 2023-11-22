using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheVoice.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Activity))]
        public int ActivityId { get; set; }

        public Activity Activity { get; set; }

        [ForeignKey(nameof(Mentor))]
        public int MentorId { get; set; }

        public Mentor Mentor { get; set; }

        [ForeignKey(nameof(Candicate))]
        public int CandicateId { get; set; }

        public Candicate Candicate { get; set; }

        [Range(0,100)]
        public int Value { get; set; }

    }
}
