namespace SCP.Common.Tools
{
    public static class EnvironmentCheck
    {
        private static readonly string ASP_ENVVARIABLE = "ASPNETCORE_ENVIRONMENT";
        private static readonly string ASP_DEVELOPMENT_ENV = "Development";

        public static bool IsDevEnv()
        {
            var environmentName = Environment.GetEnvironmentVariable(ASP_ENVVARIABLE);

            return environmentName?.Equals(ASP_DEVELOPMENT_ENV) ?? false;
        }
    }
}
