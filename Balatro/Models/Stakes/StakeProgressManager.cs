using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Balatro.Models.Stakes
{
    public class StakeProgressManager
    {
        private readonly static string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MakeshiftBalatro", "difficutlies.json");
        private static Dictionary<string, int>? UnlockedDifficulties { get; set; } = new();

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
            }
            catch(JsonException)
            {
                UnlockedDifficulties = null;
            }
            if(UnlockedDifficulties == null)
            {
                UnlockedDifficulties = new();
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
                UnlockedDifficulties["Red Deck"] = 0;
            }
  
        }
    }
}
