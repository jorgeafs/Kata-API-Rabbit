using System.Diagnostics;

namespace AsyncComms.ApiGateway
{
    public static class MessageRunner
    {
        private static Messaging.Messaging _rabbit;

        public static void Runner()
        {
            _rabbit = new Messaging.Messaging();
            for (var i = 0; i < 1000; i++)
            {
                _rabbit.SendMessage($"test-{i}:{DateTime.Now}");
            }

            _rabbit.StartReceiver();
            Receiver();
        }

        public static void Receiver()
        {
            while (true)
            {
                if (_rabbit.MessagesWithIds.Any())
                {
                    var first = _rabbit.MessagesWithIds.OrderBy(x => x.Key).FirstOrDefault();
                    Debug.WriteLine(first.Value);
                    _rabbit.ConfirmMessage(first.Key);
                }
            }
        }
    }
}
