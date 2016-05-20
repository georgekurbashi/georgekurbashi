using System;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddLevel : IFormatter
    {
        public AddLevel()
        {
            
        }
        public AddLevel(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);


            return string.Format("[Level: {0}] {1}", level, message);
        }

        private readonly IFormatter _nextFormatter;
    }
}