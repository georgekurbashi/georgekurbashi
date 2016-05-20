using System;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddDate : IFormatter
    {
        public AddDate()
        {
        }

        public AddDate(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);


            return string.Format("[{0}.{1}.{2}] {3}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, message);
        }

        private readonly IFormatter _nextFormatter;
    }
}