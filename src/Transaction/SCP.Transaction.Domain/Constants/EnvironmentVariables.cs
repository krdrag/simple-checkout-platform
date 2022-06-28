namespace SCP.Transaction.Domain.Constants
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
    }
}
