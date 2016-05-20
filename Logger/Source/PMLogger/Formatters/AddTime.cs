using System;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddTime : IFormatter
    {
        public AddTime()
        {
            
        }
        public AddTime(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);


            return string.Format("[{0}] {1}", DateTime.Now.TimeOfDay.ToString(), message);
        }

        private readonly IFormatter _nextFormatter;
    }
}