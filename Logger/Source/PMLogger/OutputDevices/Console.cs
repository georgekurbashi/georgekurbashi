using System;

namespace PMLogger.OutputDevices
{
    [Serializable]
    public class Console : IOutputDevice
    {
        public Console()
        {
            
        }

        public Console(IOutputDevice nextOutputDevice = null)
        {
            _nextOutputDevice = nextOutputDevice;
        }

        public override void Write(string message)
        {
            System.Console.WriteLine(message);

            if(_nextOutputDevice != null)
                _nextOutputDevice.Write(message);
        }

        public  IOutputDevice _nextOutputDevice;
    }
}