using TheVoice.Models;
using TheVoice.Models.ViewModels;

namespace TheVoice.Providers.Builders
{
    public static class ClassBuilder
    {
        public static MentorHomeIndexVM ConvertToMentorHomeIndexVM(Mentor mentor)
        {
            return new MentorHomeIndexVM() { Mentor = mentor };
        }
    }
}
