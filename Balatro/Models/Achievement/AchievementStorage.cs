using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Windows.Storage;

namespace Balatro.Models.Achievement
{
    public static class AchievementStorage
    {
        private const string FileName = "achievements.json";

        public static async void Save(List<Achievement> achievements)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var path = Path.Combine(folder.Path, FileName);

            var progressOnly = achievements.Select(a => new AchievementProgress
            {
                Id = a.Id,
                IsUnlocked = a.IsUnlocked,
                CurrentProgress = a.CurrentProgress,
            }).ToList();

            var json = JsonSerializer.Serialize(progressOnly);
            File.WriteAllText(path, json);
        }

        public static List<Achievement> Load()
        {
            var achievements = AchievementDefinitions.GetAll();

            var folder = ApplicationData.Current.LocalFolder;
            var path = Path.Combine(folder.Path, FileName);

            if (!File.Exists(path))
            {
                Save(achievements);
                return achievements;
            }

            try
            {
                var json = File.ReadAllText(path);
                var savedProgress = JsonSerializer.Deserialize<List<AchievementProgress>>(json) ?? new();

                foreach (var saved in savedProgress)
                {
                    var target = achievements.FirstOrDefault(a => a.Id == saved.Id);
                    if (target != null)
                    {
                        target.IsUnlocked = saved.IsUnlocked;
                        target.CurrentProgress = saved.CurrentProgress;
                    }
                }
            }
            catch { }

            return achievements;
        }
    }
}
