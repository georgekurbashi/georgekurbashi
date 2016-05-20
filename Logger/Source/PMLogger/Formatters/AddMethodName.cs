using System;

namespace PMLogger.Formatters
{
    [Serializable]
    public class AddMethodName : IFormatter
    {
        public AddMethodName()
        {
            
        }

        public AddMethodName(IFormatter nextFormatter = null)
        {
            _nextFormatter = nextFormatter;
        }

        public override string Format(string message, LogLevel level)
        {
            if (_nextFormatter != null)
                message = _nextFormatter.Format(message, level);

            //TODO : check caller parameter after changes
            return string.Format("[Method : {0}] {1}", new Caller(5).MethodName.Name, message);
        }

        private readonly IFormatter _nextFormatter;
    }
}