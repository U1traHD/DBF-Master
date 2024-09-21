using System;
using System.IO;
using Newtonsoft.Json;

public class ConfigManager
{
    public static Config LoadConfig(string filePath)
    {
        try
        {
            var json = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<Config>(json);
            return config;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"U invalid: {ex.Message}");
            return null;
        }
    }
}