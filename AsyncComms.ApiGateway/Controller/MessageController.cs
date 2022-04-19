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
        
        // GET api/<MessageController>/5
        [HttpGet("{queueId}")]
        public string Get(string queueId)
        {
            return _canDoMessages.Get(queueId);
        }

        // POST api/<MessageController>
        [HttpPost]
        public void Post([FromBody] string value, [FromBody] string queueId)
        {
            _canDoMessages.Post(queueId, value);
        }
    }
}
