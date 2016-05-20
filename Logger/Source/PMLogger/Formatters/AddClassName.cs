using System;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddClassName : IFormatter
    {
        public AddClassName()
        {
        }

        public AddClassName(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);


            return string.Format("[Class : {0}] {1}", new Caller(5).ClassName, message);
        }

        public IFormatter _nextFormatter;
    }
}