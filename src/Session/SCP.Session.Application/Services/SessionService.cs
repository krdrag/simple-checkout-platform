using MassTransit;
using SCP.Common.Models;
using SCP.Session.Application.Saga;
using SCP.Session.Application.Saga.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP.Session.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IRequestClient<IOnSessionStarted> _sessionStartClient;
        private readonly IRequestClient<IOnSessionFinished> _sessionFinishClient;
        private readonly IRequestClient<IOnSessionRequested> _sessionRequestClient;

        public SessionService(
            IRequestClient<IOnSessionStarted> sessionStartClient, 
            IRequestClient<IOnSessionFinished> sessionFinishClient, 
            IRequestClient<IOnSessionRequested> sessionRequestClient)
        {
            _sessionStartClient = sessionStartClient;
            _sessionFinishClient = sessionFinishClient;
            _sessionRequestClient = sessionRequestClient;
        }

        public async Task<SessionModel?> StartSession(WorkstationDataModel wsModel)
        {
            var (status, error) = await _sessionStartClient.GetResponse<ISessionResponse, ISessionNotFoundResponse>(new
            {
                SessionId = Guid.NewGuid(),
                WorkstationData = wsModel
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Session : null;
        }

        public async Task<SessionModel?> FinishSession(Guid id)
        {
            var (status, error) = await _sessionFinishClient.GetResponse<ISessionResponse, ISessionNotFoundResponse>(new
            {
                SessionId = id,
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Session : null;
        }

        public async Task<SessionModel?> GetSession(Guid id)
        {
            var (status, error) = await _sessionRequestClient.GetResponse<ISessionResponse, ISessionNotFoundResponse>(new
            {
                SessionId = id,
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Session : null;
        }

        
    }
}
