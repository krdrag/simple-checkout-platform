using FluentValidation.AspNetCore;
using MassTransit;
using SCP.Transaction.Application.Saga;
using SCP.Transaction.Application.Services;
using SCP.Transaction.Application.Validators;
using SCP.Transaction.Domain.Constants;

namespace SCP.Transaction.API.Extensions
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
                x.AddSagaStateMachine<TransactionSaga, TransactionSagaState>()
                    .RedisRepository(r =>
                    {
                        r.DatabaseConfiguration(redisConnStr);
                        r.KeyPrefix = "TransactionSagaState";
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
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PaymentModelValidator>());
        }

        public static void AddCustomServices(this IServiceCollection services)
        {


            // Repositories

            // Services
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
