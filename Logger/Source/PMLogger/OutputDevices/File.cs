using System;
using System.IO;

namespace PMLogger.OutputDevices
{
    [Serializable]
    public class File : IOutputDevice
    {
        public File()
        {
            
        }

        public File(string fileName, IOutputDevice nextOutputDevice = null)
        {
            _fileName = fileName;
            _nextOutputDevice = nextOutputDevice;
        }

        public override void Write(string message)
        {
            var file = new StreamWriter(_fileName, true);

            file.WriteLine(message);

            file.Close();

            if (_nextOutputDevice != null)
                _nextOutputDevice.Write(message);
        }

        public  string _fileName;

        public  IOutputDevice _nextOutputDevice;
    }
}