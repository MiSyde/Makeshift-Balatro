using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Achievement
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresAchievement : Attribute
    {
        public string AchievementId { get; }
        public RequiresAchievement(string achievementId) => AchievementId = achievementId;
    }
}
