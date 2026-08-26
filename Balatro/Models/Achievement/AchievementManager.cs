using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balatro.Models.Achievement
{
    public class AchievementManager
    {
        private readonly List<Achievement> achievements;

        public AchievementManager(List<Achievement> Achievements)
        {
            achievements = Achievements;
            EventReporter.StatChanged += OnStatChanged;
            EventReporter.ActionPerformed += OnActionPerformed;
        }

        private void OnActionPerformed(string actionKey)
        {
            var ach = achievements.FirstOrDefault(a => !a.IsUnlocked && a.StatKey == actionKey);
            if (ach != null)
                Unlock(ach);
        }

        private void OnStatChanged(string actionKey, int amount)
        {
            foreach (Achievement ach in achievements.Where(a => !a.IsUnlocked && a.StatKey == actionKey))
            {
                ach.CurrentProgress += amount;
                if (ach.CurrentProgress >= ach.TargetValue)
                    Unlock(ach);
            }
        }

        private void Unlock(Achievement ach)
        {
            ach.IsUnlocked = true;
            SaveProgress();
        }

        public void SaveProgress() => AchievementStorage.Save(achievements);
        public void LoadProgress()
        {
            var loaded = AchievementStorage.Load();
            foreach (var saved in loaded)
            {
                var target = achievements.FirstOrDefault(a => a.Id == saved.Id);
                if (target != null)
                {
                    target.IsUnlocked = saved.IsUnlocked;
                    target.CurrentProgress = saved.CurrentProgress;
                }
            }
        }

        public bool IsUnlocked(string achievementId) => achievements.FirstOrDefault(a => a.Id == achievementId)?.IsUnlocked ?? false;
    }
}
