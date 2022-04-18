namespace AsyncComms.ApiGateway.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {  
        private readonly ICanDoMessages _canDoMessages;

        public MessageController(ICanDoMessages canDoMessages)
        {
            _canDoMessages = canDoMessages ?? throw new ArgumentNullException(typeof(ICanDoMessages).ToString());
        }
        // GET: api/<MessageController>
        [HttpGet("{pageNumber}/{pageSize}")]
        public IEnumerable<string> GetAll(int pageNumber,int pageSize)
        {
            return _canDoMessages.GetAll(pageNumber, pageSize);
        }

        // GET api/<MessageController>/5
        [HttpGet("{index}")]
        public string Get(int index)
        {
            return _canDoMessages.Get(index);
        }

        // POST api/<MessageController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _canDoMessages.Post(value);
        }

        // PUT api/<MessageController>/5
        [HttpPut("{index}")]
        public void Put(int index, [FromBody] string value)
        {
            _canDoMessages.Put(index,value);
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{index}")]
        public void Delete(int index)
        {
            _canDoMessages.Delete(index);
        }
    }
}
