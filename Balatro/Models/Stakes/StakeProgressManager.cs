using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Windows.UI;

namespace Balatro.Models.Stakes
{
    public class StakeProgressManager
    {
        private readonly static string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MakeshiftBalatro", "difficutlies.json");
        private static Dictionary<string, int>? UnlockedDifficulties { get; set; } = new();
        public static Dictionary<int, SolidColorBrush> Colors { get; } = new();

        public StakeProgressManager()
        {
            Load();
        }

        public static int GetDiff(string deckId)
        {
            return UnlockedDifficulties![deckId];
        }

        public void Unlock(string deckId, int UpdateDiff)
        {
            UnlockedDifficulties![deckId] = UpdateDiff;
            Save();
        }

        public static void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
            File.WriteAllText(_path, JsonSerializer.Serialize(UnlockedDifficulties, new JsonSerializerOptions { WriteIndented = true}));
        }

        public static void Load()
        {
            if (!File.Exists(_path)) return;
            try
            {
                UnlockedDifficulties = JsonSerializer.Deserialize<Dictionary<string, int>>(File.ReadAllText(_path));
                InitColors();
            }
            catch(JsonException)
            {
                UnlockedDifficulties = null;
            }
            if(UnlockedDifficulties == null || UnlockedDifficulties.Count == 0)
            {
                UnlockedDifficulties = new();
                UnlockedDifficulties["Red Deck"] = 0;

                if (Colors.Count == 0) 
                    InitColors();
            }
  
        }

        private static void InitColors()
        {
            Colors[0] = new SolidColorBrush(Color.FromArgb(255, 235, 246, 248));
            Colors[1] = new SolidColorBrush(Color.FromArgb(255, 253, 95, 85));
            Colors[2] = new SolidColorBrush(Color.FromArgb(255, 85, 163, 131));
            Colors[3] = new SolidColorBrush(Color.FromArgb(255, 0, 156, 253));
            Colors[4] = new SolidColorBrush(Color.FromArgb(255, 79, 99, 103));
            Colors[5] = new SolidColorBrush(Color.FromArgb(255, 138, 113, 225));
            Colors[6] = new SolidColorBrush(Color.FromArgb(255, 228, 124, 76));
            Colors[7] = new SolidColorBrush(Color.FromArgb(255, 242, 194, 85));
        }
    }
}
