namespace SCP.Session.Application.Saga.Events
{
    public interface IOnSessionFinished
    {
        public Guid SessionId { get; set; }
    }
}
