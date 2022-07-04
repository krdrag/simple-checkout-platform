namespace SCP.Common.Models
{
    public class SessionModel
    {
        public Guid SessionId { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeFinished { get; set; }
        public WorkstationDataModel WorkstationData { get; set; } = new WorkstationDataModel();
    }
}
