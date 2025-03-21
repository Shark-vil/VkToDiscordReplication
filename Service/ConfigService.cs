﻿using System.Text.Json;
using VkToDiscordReplication.Models.Config;

namespace VkToDiscordReplication.Service
{
    internal static class ConfigService
    {
        internal static async Task<AppConfigData> GetConfigAsync()
        {
            AppConfigData? config;
            string jsonString;
            string appConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (!File.Exists(appConfigPath))
            {
                config = new AppConfigData();
                jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(appConfigPath, jsonString);
            }
            else
            {
                jsonString = await File.ReadAllTextAsync(appConfigPath);
                config = JsonSerializer.Deserialize<AppConfigData>(jsonString);
            }

            if (config == null)
                throw new Exception("An error occurred while trying to convert JSON to a AppDataModel config object.");

            return config;
        }
    }
}
