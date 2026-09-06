using Balatro.Enums;
using Balatro.Models.Achievement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Balatro.Util
{
    public static class Helper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute? attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return string.Empty;
        }

        public static string XDashY(int x, int y) => x.ToString() + "/" + y.ToString();

        public static List<T> GenerateClassesInNamespace<T>(string @namespace)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == @namespace && t.IsClass && typeof(T).IsAssignableFrom(t)
                && !t.IsAbstract && t.GetConstructors().Any(c => c.GetParameters().All(p => p.HasDefaultValue)))
                .Select(t => (T)Activator.CreateInstance(t)!).ToList();
        }

        public static List<T> GenerateUnlocked<T>(string @namespace) where T : class
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == @namespace
                            && typeof(T).IsAssignableFrom(t)
                            && !t.IsAbstract && t.GetConstructors().Any(c => c.GetParameters().All(p => p.HasDefaultValue)));

            var result = new List<T>();

            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<RequiresAchievement>();
                bool isUnlocked = (attr == null || App.AchievementManager.IsUnlocked(attr.AchievementId));

                if (isUnlocked)
                    result.Add((T)Activator.CreateInstance(type)!);
            }

            return result;
        }

        public static List<(T instance, bool isUnlocked, string? requiredAchievement)> GenerateLocked<T>(string @namespace) where T : class
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == @namespace
                            && typeof(T).IsAssignableFrom(t)
                            && !t.IsAbstract && t.GetConstructors().Any(c => c.GetParameters().All(p => p.HasDefaultValue)));

            var result = new List<(T, bool, string?)>();

            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<RequiresAchievement>();
                bool isUnlocked = (attr == null || App.AchievementManager.IsUnlocked(attr.AchievementId));
                var instance = (T)Activator.CreateInstance(type)!;
                result.Add((instance, isUnlocked, attr?.AchievementId));
            }

            return result;
        }
    }
}
