using System;
using System.Threading;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddThreadId : IFormatter
    {
        public AddThreadId()
        {
            
        }
        public AddThreadId(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);

            return string.Format("[ThreadID = {0}] {1}", Thread.CurrentThread.ManagedThreadId, message);
        }

        private readonly IFormatter _nextFormatter;
    }
}
