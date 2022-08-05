using FluentValidation.AspNetCore;
using MassTransit;
using SCP.Common.Constants;
using SCP.Transaction.Application.Saga;
using SCP.Transaction.Application.Services;
using SCP.Transaction.Application.Validators;
using SCP.Transaction.Domain.Interfaces;
using SCP.Transaction.Infrastructure.Repositories;
using StackExchange.Redis;

namespace SCP.Transaction.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddExternalServices(this IServiceCollection services)
        {
            var redisConnStr = $"{EnvironmentVariables.RedisHost},password={EnvironmentVariables.RedisPwd}";

            var multiplexer = ConnectionMultiplexer.Connect(redisConnStr);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

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

                x.AddConsumers(typeof(ServiceCollectionExtensions).Assembly);

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

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options => {
                    options.ApiName = "SCP.Transaction";
                    options.Authority = "https://localhost:6100";
                });
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ITransactionListCacheRepository, TransactionListCacheRepository>();

            // Services
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
