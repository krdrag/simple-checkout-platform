using MassTransit;
using SCP.Common.Models;
using SCP.Session.Application.Saga.Events;

namespace SCP.Session.Application.Saga
{
    public static class SessionSagaOperations
    {
        public static void CreateSession(BehaviorContext<SessionSagaState, IOnSessionStarted> context)
        {
            context.Saga.Session = new SessionModel()
            {
                SessionId = context.Message.SessionId,
                TimeStarted = DateTime.Now,
                WorkstationData = context.Message.WorkstationData
            };
        }
    }
}
