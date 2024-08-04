namespace DP_manager_API.Configuration;

using System;
using System.IO;

public static class DotEnv
{
    static List<string> variables = new List<string>();

    public static bool HasVariable(string name)
    {
        return variables.Contains(name);
    }

    public static string GetVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }

    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(
                '=', 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            variables.Add(parts[0]);
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}