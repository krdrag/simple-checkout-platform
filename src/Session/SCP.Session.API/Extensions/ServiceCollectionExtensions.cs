using FluentValidation.AspNetCore;
using MassTransit;
using SCP.Common.Constants;
using SCP.Session.Application.Saga;
using SCP.Session.Application.Services;
using SCP.Session.Application.Validators;

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

            services.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<SessionSaga, SessionSagaState>()
                    .RedisRepository(r =>
                    {
                        r.DatabaseConfiguration(redisConnStr);
                        r.KeyPrefix = "SessionSagaState";
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

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<WorkstationDataModelValidator>());

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options => {
                    options.ApiName = "SCP.Session";
                    options.Authority = EnvironmentVariables.IdentityAuthority;
                    // For now disable requirement for https
                    options.RequireHttpsMetadata = false;
                });
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            // Repositories

            // Services
            services.AddScoped<ISessionService, SessionService>();
        }
    }
}
