namespace SCP.Session.Application.Saga.Events
{
    public interface IOnSessionRequested
    {
        public Guid SessionId { get; set; }
    }
}
