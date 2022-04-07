namespace AsyncComms.ApiGateway.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {  
        // GET: api/<MessageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _messages;
        }

        // GET api/<MessageController>/5
        [HttpGet("{index}")]
        public string Get(int index)
        {
            return _messages[index];
        }

        // POST api/<MessageController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _messages.Add(value);
        }

        // PUT api/<MessageController>/5
        [HttpPut("{index}")]
        public void Put(int index, [FromBody] string value)
        {
            _messages[index] = value;
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{index}")]
        public void Delete(int index)
        {
            _messages.RemoveAt(index);
        }
    }
}
