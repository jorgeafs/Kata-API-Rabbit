using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncComms.ApiGateway.BusinessLogic.Contracts
{
    public interface ICanDoMessages
    {
        public string Get(string queueId);
        public void Post(string queueId, string message);
    }
}
