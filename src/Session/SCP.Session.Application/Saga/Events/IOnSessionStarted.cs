using SCP.Common.Models;

namespace SCP.Session.Application.Saga.Events
{
    public interface IOnSessionStarted
    {
        Guid SessionId { get; set; }

        public WorkstationDataModel WorkstationData { get; }
    }
}
