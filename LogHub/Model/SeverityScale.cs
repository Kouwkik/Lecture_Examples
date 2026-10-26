using System.Collections.Generic;

namespace LogHub.Model
{
    // Единственное место, где живёт шкала уровней. В god-классе это знание
    // было зашито в метод SeverityToInt и дублировало порядок уровней.
    public static class SeverityScale
    {
        private static readonly Dictionary<string, int> Ranks = new Dictionary<string, int>
        {
            { "Debug", 0 },
            { "Info", 1 },
            { "Warning", 2 },
            { "Error", 3 },
            { "Critical", 4 }
        };

        public static int ToRank(string severity)
        {
            if (severity != null && Ranks.TryGetValue(severity, out var rank))
            {
                return rank;
            }

            // Поведение исходного god-класса: неизвестный уровень = Info.
            return 1;
        }
    }
}
