namespace SCP.Common.Constants
{
    public static class EnvironmentVariables
    {
        // Redis
        public static string RedisHost => Environment.GetEnvironmentVariable("REDIS_HOST") ?? throw new KeyNotFoundException("Redis host environment variable not found!");
        public static string RedisPwd => Environment.GetEnvironmentVariable("REDIS_PWD") ?? throw new KeyNotFoundException("Redis password environment variable not found!");

        // RabbitMQ
        public static string RabbitHostVar => Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? throw new KeyNotFoundException("Rabbit host environment variable not found!");
        public static string RabbitUserEnvVar => Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? throw new KeyNotFoundException("Rabbit user environment variable not found!");
        public static string RabbitPassEnvVar => Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? throw new KeyNotFoundException("Rabbit password environment variable not found!");

        // Identity
        public static string IdentityAuthority => Environment.GetEnvironmentVariable("IDENTITY_AUTHORITY") ?? throw new KeyNotFoundException("Authority variable not found!");
        public static string POSClientPasswordEnvVar => Environment.GetEnvironmentVariable("IDENTITY_POS_CLIENT_PASSWORD") ?? throw new KeyNotFoundException("Pos client password environment variable not found!");
        public static string PostmanClientPasswordEnvVar => Environment.GetEnvironmentVariable("IDENTITY_POSTMAN_CLIENT_PASSWORD") ?? throw new KeyNotFoundException("Postman client password host environment variable not found!");
        public static string TransactionResourceEnvVar => Environment.GetEnvironmentVariable("IDENTITY_TRANSACTION_RESOURCE_PASSWORD") ?? throw new KeyNotFoundException("Transaction service client password host environment variable not found!");
        public static string SessionResourceEnvVar => Environment.GetEnvironmentVariable("IDENTITY_SESSION_RESOURCE_PASSWORD") ?? throw new KeyNotFoundException("Session service client password environment variable not found!");
    }
}
