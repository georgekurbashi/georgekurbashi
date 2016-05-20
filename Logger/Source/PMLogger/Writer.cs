using System;

namespace PMLogger
{
    [Serializable]
    public class Writer
    {
        public Writer()
        {

        }

        public Writer(IFormatter formatter, IOutputDevice outputDevice)
        {
            _formatter = formatter;
            _outputDevice = outputDevice;
        }

        public void Write(string message, LogLevel level)
        {
            _outputDevice.Write(_formatter.Format(message, level));
        }

        public IFormatter _formatter;
        public IOutputDevice _outputDevice;
    }
}