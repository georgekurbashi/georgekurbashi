namespace PMLogger
{
    public enum LogLevel { Off, Error, Warning, Info, Debug }

    public class Logger
    {//TODO:singleton?
        public Logger(string levelBaseFileName = null, string writerBaseFileName = null)
        {
            LevelBase = new LevelBase(levelBaseFileName);
            WriterBase = new WriterBase(writerBaseFileName);
        }

        public Logger(LevelBase levelBase, WriterBase writerBase)
        {
            LevelBase = levelBase;
            WriterBase = writerBase;
        }

        public void LogError(string message)
        {
            var caller = new Caller(2);

            var setLevel = LevelBase.GetLevel(caller);

            if (setLevel >= LogLevel.Error)
                Write(message, LogLevel.Error, caller);
        }

        public void LogWarning(string message)
        {
            var caller = new Caller(2);

            var setLevel = LevelBase.GetLevel(caller);

            if (setLevel >= LogLevel.Warning)
                Write(message, LogLevel.Warning, caller);
        }

        public void LogInfo(string message)
        {
            var caller = new Caller(2);

            var setLevel = LevelBase.GetLevel(caller);

            if (setLevel >= LogLevel.Info)
                Write(message, LogLevel.Info, caller);
        }

        public void LogDebug(string message)
        {
            var caller = new Caller(2);

            var setLevel = LevelBase.GetLevel(caller);

            if (setLevel >= LogLevel.Debug)
                Write(message, LogLevel.Debug, caller);
        }

        private void Write(string message, LogLevel level, Caller caller)
        {
            //TODO : maybe class "Message" with text of message and its level
            
            WriterBase.GetWriter(caller).Write(message, level);
        }

        public WriterBase WriterBase;
        public LevelBase LevelBase;
    }
}