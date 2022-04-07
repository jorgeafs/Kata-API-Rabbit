using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncComms.ApiGateway.BusinessLogic.Contracts;

namespace AsyncComms.ApiGateway.BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private List<string> _messages = new() { "value1", "value2" };
    }
}
