using System.Xml.Serialization;
using PMLogger.OutputDevices;

namespace PMLogger
{
    [XmlInclude(typeof(Console)), XmlInclude(typeof(File))]
    public abstract class IOutputDevice
    {
        public abstract void Write(string message);
       
    }
}