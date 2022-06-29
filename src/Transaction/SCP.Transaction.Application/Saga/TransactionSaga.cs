using MassTransit;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Saga.Events;
using SCP.Transaction.Domain.Constants;
using SCP.Transaction.Domain.Exceptions;

namespace SCP.Transaction.Application.Saga
{
    public class TransactionSaga : MassTransitStateMachine<TransactionSagaState>
    {
        public TransactionSaga()
        {
            ConfigureEvents();
            ConfigureStates();

            InstanceState(x => x.CurrentState);

            Initially(
                When(OnStart)
                    .Then(TransactionSagaOperations.CreateTransaction())
                    .RespondAsync(async x => await x.Init<ITransactionResponse>(new
                    {
                        x.Saga.Transaction
                    }))
                    .TransitionTo(Started));

            DuringAny(
                When(OnGet)
                    .RespondAsync(x => x.Init<ITransactionResponse>(new
                    {
                        x.Saga.Transaction
                    })));

            During(Started,
                When(OnStart)
                    .Then(x => throw new IncorrectSagaStateException()),
                When(OnFinish)
                    .Then(x => throw new IncorrectSagaStateException()),
                When(OnUpdate, context => !context.Message.SwitchToTotal)
                    .Then(TransactionSagaOperations.AddArticles())
                    .RespondAsync(x => x.Init<ITransactionResponse>(new
                    {
                        x.Saga.Transaction
                    })),
                When(OnUpdate, context => context.Message.SwitchToTotal)
                    .Then(TransactionSagaOperations.SwtichToTotal())
                    .RespondAsync(x => x.Init<ITransactionResponse>(new
                    {
                        x.Saga.Transaction
                    }))
                    .TransitionTo(Total));

            During(Total,
                When(OnStart)
                    .Then(x => throw new IncorrectSagaStateException()),
                 When(OnUpdate)
                    .Then(TransactionSagaOperations.AddPayments())
                    .RespondAsync(x => x.Init<ITransactionResponse>(new
                    {
                        x.Saga.Transaction
                    })),
                When(OnFinish)
                    .Then(x =>
                    {
                        x.Saga.Transaction.State = TransactionConstants.State.Finished;
                        x.Saga.Transaction.TimeFinished = DateTime.UtcNow;
                    })
                    .RespondAsync(x => x.Init<ITransactionResponse>(new
                    {
                        x.Saga.Transaction
                    }))
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        private void ConfigureEvents()
        {
            Event(() => OnStart, x => x.CorrelateById(m => m.Message.TransactionId));
            Event(() => OnFinish, x =>
            {
                x.CorrelateById(m => m.Message.TransactionId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<ITransactionNotFoundResponse>(new { });
                    }
                }));
            });
            Event(() => OnUpdate, x =>
            {
                x.CorrelateById(m => m.Message.TransactionId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<ITransactionNotFoundResponse>(new { });
                    }
                }));
            });
            Event(() => OnGet, x =>
            {
                x.CorrelateById(m => m.Message.TransactionId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<ITransactionNotFoundResponse>(new { });
                    }
                }));
            });
        }

        private void ConfigureStates()
        {
            State(() => Started);
            State(() => Total);
        }

        public State? Started { get; set; }
        public State? Total { get; set; }

        public Event<IOnTransactionStarted>? OnStart { get; set; }
        public Event<IOnTransactionFinished>? OnFinish { get; set; }
        public Event<IOnTransactionUpdated>? OnUpdate { get; set; }
        public Event<IOnTransactionGet>? OnGet { get; set; }
    }
}
