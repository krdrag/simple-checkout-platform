﻿namespace SCP.Common.Tools
{
    /// <summary>
    /// Load .env.dev file. Loads all varaibles into environment.
    /// Used only for services running locally.
    /// </summary>
    public static class DevEnvLoader
    {
        private static readonly string ASP_ENVVARIABLE = "ASPNETCORE_ENVIRONMENT";
        private static readonly string ASP_DEVELOPMENT_ENV = "Development";
        private static readonly string[] _defaultKeysToIgnore = new string[] { };

        public static void Load(IEnumerable<string>? providedKeysToIgnore = null)
        {
            var keysToIgnore = _defaultKeysToIgnore;

            var environmentName = Environment.GetEnvironmentVariable(ASP_ENVVARIABLE);

            if (!environmentName?.Equals(ASP_DEVELOPMENT_ENV) ?? true)
                return;

            if (providedKeysToIgnore != null && providedKeysToIgnore.Any())
                keysToIgnore = _defaultKeysToIgnore.Concat(providedKeysToIgnore).ToArray();

            var path = GetPathToEnvFile();
            if (!File.Exists(path))
                return;

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                if (keysToIgnore.Any(x => x.Equals(parts[0])))
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }

        }
        private static string GetPathToEnvFile()
        {
            var path = Directory.GetCurrentDirectory();

            int index = path.IndexOf(@"\src\");
            if (index >= 0)
                path = path.Remove(index);

            return $@"{path}\.env.dev";
        }
    }
}
