using AsyncComms.ApiGateway.BusinessLogic.Contracts;
using AsyncComms.Messaging;

namespace AsyncComms.ApiGateway.BusinessLogic.Services
{
    public class CanDoMessages : ICanDoMessages
    {
        private readonly IMessaging _messages;

        public CanDoMessages(IMessaging messaging)
        {
            _messages = messaging;
        }

        public string Get(string queueId)
        {
            return _messages.ReceiveOne(queueId);
        }

        public void Post(string queueId, string message)
        {
            _messages.SendSingleMessage(message,queueId);
        }
    }
}
