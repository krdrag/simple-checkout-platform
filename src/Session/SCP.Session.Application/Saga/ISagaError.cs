namespace SCP.Session.Application.Saga
{
    public interface ISagaError
    {
        string Code { get; set; }
        string Message { get; set; }
    }
}
