namespace SCP.Common.Exceptions
{
    public class SessionNotFoundException : BaseException
    {
        public SessionNotFoundException() : base("SESSION_NOT-FOUND", "Session not found")
        {
        }
    }
}
