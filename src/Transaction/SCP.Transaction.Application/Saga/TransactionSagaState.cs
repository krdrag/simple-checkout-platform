using MassTransit;
using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Saga
{
    public class TransactionSagaState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }
        public TransactionModel Transaction { get; set; } = new TransactionModel();
        public int CurrentLineNumber { get; set; }
        public int Version { get; set; }
    }
}
