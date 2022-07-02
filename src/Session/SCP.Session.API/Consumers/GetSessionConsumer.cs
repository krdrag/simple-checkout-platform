using MassTransit;
using SCP.MessageBus.Session;
using SCP.Session.Application.Services;

namespace SCP.Session.API.Consumers
{
    public class GetSessionConsumer : IConsumer<GetSessionRequest>
    {
        private readonly ISessionService _sessionService;

        public GetSessionConsumer(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task Consume(ConsumeContext<GetSessionRequest> context)
        {
            var request = context.Message;
            var result = await _sessionService.GetSession(request.SessionId);

            await context.RespondAsync(new GetSessionResponse
            {
                Session = result
            });
        }
    }
}
