﻿using MassTransit;
using SCP.Common.Constants;
using SCP.Session.Application.Saga;

namespace SCP.Session.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddExternalServices(this IServiceCollection services)
        {
            var redisConnStr = $"{EnvironmentVariables.RedisHost},password={EnvironmentVariables.RedisPwd}";
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnStr;
            });

            var rUsr = EnvironmentVariables.RabbitUserEnvVar;
            var rPwd = EnvironmentVariables.RabbitPassEnvVar;
            var rHost = EnvironmentVariables.RabbitHostVar;

            services.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<SessionSaga, SessionSagaState>()
                    .RedisRepository(r =>
                    {
                        r.DatabaseConfiguration(redisConnStr);
                        r.KeyPrefix = "SessionSagaState";
                    });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.Host(EnvironmentVariables.RabbitHostVar, settings =>
                    {
                        settings.Username(EnvironmentVariables.RabbitUserEnvVar);
                        settings.Password(EnvironmentVariables.RabbitPassEnvVar);

                    });
                });
            });
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            // Repositories

            // Services
        }
    }
}