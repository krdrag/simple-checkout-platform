using SCP.Common.Exceptions;

namespace SCP.Session.Domain.Exceptions
{
    public class SessionCannotBeClosedException : BaseException
    {
        public SessionCannotBeClosedException() : base("SESSION_SESSION-CANNOT-CLOSE", "Session cannot be closed, it still has opened transactions.")
        {
        }
    }
}
