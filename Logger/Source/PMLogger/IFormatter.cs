using System.Xml.Serialization;
using PMLogger.Formatters;

namespace PMLogger
{
    [XmlInclude(typeof(AddClassName)), XmlInclude(typeof(AddMethodName)), XmlInclude(typeof(AddThreadId)), XmlInclude(typeof(AddTime)), XmlInclude(typeof(AddDate)), XmlInclude(typeof(AddLevel))]
    public abstract class IFormatter
    {
        public abstract string Format(string message, LogLevel level);
    }
}