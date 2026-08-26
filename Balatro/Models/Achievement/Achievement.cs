using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Achievement
{
    public class Achievement : AchievementProgress
    {
        public string StatKey { get; set; }
        public string Description { get; set; }
        public int TargetValue { get; set; }
    }
}
