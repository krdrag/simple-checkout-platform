using MassTransit;
using SCP.Common.Models;

namespace SCP.Session.Application.Saga
{
    public class SessionSagaState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }
        public SessionModel Session { get; set; } = new SessionModel();
        public int Version { get; set; }
    }
}
