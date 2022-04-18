using AsyncComms.ApiGateway.BusinessLogic.Contracts;
using AsyncComms.Messaging;

namespace AsyncComms.ApiGateway.BusinessLogic.Services
{
    public class CanDoMessages : ICanDoMessages
    {
        private List<string> _messages = new() { "value1", "value2" };

        public CanDoMessages(IMessaging )
        {
            
        }

        public IEnumerable<string> GetAll(int pageNumber, int pageSize)
        {
            return _messages.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public string Get(int index)
        {
            return _messages[index];
        }

        public void Post(string message)
        {
            _messages.Add(message);
        }

        public void Put(int index, string message)
        {
            _messages[index] = message;
        }

        public void Delete(int index)
        {
            _messages.Remove(_messages[index]);
        }
    }
}
