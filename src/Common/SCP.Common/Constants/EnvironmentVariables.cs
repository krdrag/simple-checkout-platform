namespace SCP.Common.Constants
{
    public static class EnvironmentVariables
    {
        // Redis
        public static string RedisHost => Environment.GetEnvironmentVariable("REDIS_HOST") ?? string.Empty;
        public static string RedisPwd => Environment.GetEnvironmentVariable("REDIS_PWD") ?? string.Empty;

        // RabbitMQ
        public static string RabbitHostVar => Environment.GetEnvironmentVariable("RabbitMQ_Host") ?? string.Empty;
        public static string RabbitUserEnvVar => Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? string.Empty;
        public static string RabbitPassEnvVar => Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? string.Empty;

        // Identity
        public static string POSClientPasswordEnvVar => Environment.GetEnvironmentVariable("POS_CLIENT_PASSWORD") ?? string.Empty;
        public static string PostmanClientPasswordEnvVar => Environment.GetEnvironmentVariable("POSTMAN_CLIENT_PASSWORD") ?? string.Empty;
        public static string TransactionResourceEnvVar => Environment.GetEnvironmentVariable("TRANSACTION_RESOURCE_PASSWORD") ?? string.Empty;
        public static string SessionResourceEnvVar => Environment.GetEnvironmentVariable("SESSION_RESOURCE_PASSWORD") ?? string.Empty;
    }
}
