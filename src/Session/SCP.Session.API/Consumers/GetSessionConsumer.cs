using MassTransit;
using SCP.Common.Exceptions;
using SCP.MessageBus.Session;
using SCP.Session.Application.Services;

namespace SCP.Session.API.Consumers
{
    public class GetSessionConsumer : IConsumer<GetSessionRequest>
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger _logger;

        public GetSessionConsumer(ISessionService sessionService, ILogger<GetSessionConsumer> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetSessionRequest> context)
        {
            try
            {
                var request = context.Message;
                var result = await _sessionService.GetSession(request.SessionId);

                await context.RespondAsync(new GetSessionResponse
                {
                    Session = result
                });
            } 
            catch(BaseException ex)
            {
                _logger.LogInformation($"{ex.ErrorCode}, {ex.Message}");
                _logger.LogDebug(ex.InnerException?.ToString());

                await context.RespondAsync(new GetSessionResponse
                {
                    Session = null
                });
            }
        }
    }
}
