using MassTransit;
using SCP.Common.Exceptions;
using SCP.Session.Application.Saga.Events;

namespace SCP.Session.Application.Saga
{
    public class SessionSaga : MassTransitStateMachine<SessionSagaState>
    {
        public SessionSaga()
        {
            ConfigureEvents();
            ConfigureStates();

            InstanceState(x => x.CurrentState);

            Initially(
                When(OnStart)
                    .Then(SessionSagaOperations.CreateSession)
                    .RespondAsync(x => x.Init<ISessionResponse>(new
                    {
                        x.Saga.Session
                    }))
                    .TransitionTo(Started));

            DuringAny(
                When(OnGetSession)
                    .RespondAsync(x => x.Init<ISessionResponse>(new
                    {
                        x.Saga.Session
                    })));

            During(Started,
                When(OnStart)
                    .Then(x => throw new IncorrectSagaStateException()),
                When(OnFinish)
                    .RespondAsync(x => x.Init<ISessionResponse>(new
                    {
                        x.Saga.Session
                    }))
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        private void ConfigureEvents()
        {
            Event(() => OnStart, x => x.CorrelateById(m => m.Message.SessionId));
            Event(() => OnFinish, x =>
            {
                x.CorrelateById(m => m.Message.SessionId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<ISessionNotFoundResponse>(new { });
                    }
                }));
            });
            Event(() => OnGetSession, x =>
            {
                x.CorrelateById(m => m.Message.SessionId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<ISessionNotFoundResponse>(new { });
                    }
                }));
            });
        }

        private void ConfigureStates()
        {
            State(() => Started);
        }

        public State? Started { get; set; }

        public Event<IOnSessionStarted>? OnStart { get; set; }
        public Event<IOnSessionFinished>? OnFinish { get; set; }
        public Event<IOnSessionRequested>? OnGetSession { get; set; }
    }
}
