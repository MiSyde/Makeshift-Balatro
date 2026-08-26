using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models
{
    public class EventReporter
    {
        public static event Action<string, int>? StatChanged;
        public static event Action<string>? ActionPerformed;

        public static void ReportStat(string statName, int amount = 1) => StatChanged?.Invoke(statName, amount);

        public static void ReportAction(string actionName) => ActionPerformed?.Invoke(actionName);
    }
}
