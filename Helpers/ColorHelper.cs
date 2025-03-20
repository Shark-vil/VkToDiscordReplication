using System.Globalization;

namespace VkToDiscordReplication.Helpers
{
    internal static class ColorHelper
    {
        internal static int? ConvertHexToInt(string hex)
        {
            try
            {
                hex = hex.TrimStart('#');
                if (hex.Length == 3)
                    hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}";
                return int.Parse(hex, NumberStyles.HexNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex}");
            }
            return null;
        }
    }
}
