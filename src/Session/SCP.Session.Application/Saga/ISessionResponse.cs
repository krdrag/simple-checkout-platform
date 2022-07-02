using SCP.Common.Models;

namespace SCP.Session.Application.Saga
{
    public interface ISessionResponse
    {
        public SessionModel Session { get; set; }
    }
}
