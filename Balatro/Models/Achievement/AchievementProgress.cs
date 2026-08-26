using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Achievement
{
    public class AchievementProgress
    {
        public string Id { get; set; } = "";
        public bool IsUnlocked { get; set; }
        public int CurrentProgress { get; set; }
    }
}
