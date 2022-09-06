using MassTransit;
using SCP.Common.Exceptions;
using SCP.Common.Models;
using SCP.MessageBus.Transaction;
using SCP.Session.Application.Saga;
using SCP.Session.Application.Saga.Events;
using SCP.Session.Domain.Exceptions;

namespace SCP.Session.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IRequestClient<IOnSessionStarted> _sessionStartClient;
        private readonly IRequestClient<IOnSessionFinished> _sessionFinishClient;
        private readonly IRequestClient<IOnSessionRequested> _sessionRequestClient;

        private readonly IRequestClient<GetTransactionListRequest> _transactionClient;
        private readonly IRequestClient<ClearTransactionListRequest> _transactionClearClient;

        public SessionService(
            IRequestClient<IOnSessionStarted> sessionStartClient, 
            IRequestClient<IOnSessionFinished> sessionFinishClient, 
            IRequestClient<IOnSessionRequested> sessionRequestClient, 
            IRequestClient<GetTransactionListRequest> transactionClient, 
            IRequestClient<ClearTransactionListRequest> transactionClearClient)
        {
            _sessionStartClient = sessionStartClient;
            _sessionFinishClient = sessionFinishClient;
            _sessionRequestClient = sessionRequestClient;
            _transactionClient = transactionClient;
            _transactionClearClient = transactionClearClient;
        }

        public async Task<SessionModel?> StartSession(WorkstationDataModel wsModel)
        {
            var (status, error) = await _sessionStartClient.GetResponse<ISessionResponse, ISagaError>(new
            {
                SessionId = Guid.NewGuid(),
                WorkstationData = wsModel
            });

            if (!status.IsCompletedSuccessfully)
            {
                var errorResp = error.Result.Message;
                throw new BaseException(errorResp.Code, errorResp.Message);
            }

            return status.IsCompletedSuccessfully ? status.Result.Message.Session : null;
        }

        public async Task<SessionModel?> FinishSession(Guid id)
        {
            var result = await _transactionClient.GetResponse<GetTransactionListResponse>(new GetTransactionListRequest
            {
                SessionId = id
            });

            if (result == null || result.Message == null || result.Message.TransactionList == null)
                throw new Common.Exceptions.MessageException();

            if (result.Message.TransactionList.Any())
                throw new SessionCannotBeClosedException();

            var (status, error) = await _sessionFinishClient.GetResponse<ISessionResponse, ISagaError>(new
            {
                SessionId = id,
            });

            if (!status.IsCompletedSuccessfully)
            {
                var errorResp = error.Result.Message;
                throw new BaseException(errorResp.Code, errorResp.Message);
            }

            return status.IsCompletedSuccessfully ? status.Result.Message.Session : null;
        }

        public async Task<SessionModel?> GetSession(Guid id)
        {
            var (status, error) = await _sessionRequestClient.GetResponse<ISessionResponse, ISagaError>(new
            {
                SessionId = id,
            });

            if(!status.IsCompletedSuccessfully)
            {
                var errorResp = error.Result.Message;
                throw new BaseException(errorResp.Code, errorResp.Message);
            }

            return status.IsCompletedSuccessfully ? status.Result.Message.Session : null;
        }

        public async Task ClearSession(Guid id)
        {
            await _transactionClearClient.GetResponse<ClearTransactionListResponse>(new ClearTransactionListRequest
            {
                Sessionid = id
            });
        }
    }
}
