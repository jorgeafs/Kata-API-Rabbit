using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncComms.ApiGateway.BusinessLogic.Contracts
{
    public interface ICanDoMessages
    {
        public IEnumerable<string> GetAll(int pageNumber, int pageSize);
        public string Get(int index);
        public void Post(string message);
        public void Put(int index, string message);
        public void Delete(int index);
    }
}
