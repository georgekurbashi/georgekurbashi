using System;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddProcessId : IFormatter
    {
        public AddProcessId()
        {
            
        }
        public AddProcessId(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);


            return string.Format("[ProcessID = {0}] {1}", System.Diagnostics.Process.GetCurrentProcess().Id, message);
        }

        private readonly IFormatter _nextFormatter;
    }
}