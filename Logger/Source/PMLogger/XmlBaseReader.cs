using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace PMLogger
{
    public class XmlBaseReader
    {
        public XmlBaseReader(string fileName)
        {
            _fileName = fileName;
        }

        public Dictionary<string, SerialazibleDictionary<string, LogLevel>> ReadLevelBase(out LogLevel defaultLevel)
        {
            //TODO: here can be exceptions

            string[] targetNames = {"classes", "assemblies"};

            var baseToRead = new Dictionary<string, SerialazibleDictionary<string, LogLevel>>();

            var readerSettings = new XmlReaderSettings { IgnoreWhitespace = true };

            var reader = XmlReader.Create(_fileName, readerSettings);

            reader.Read();//header
            reader.Read();//document root
            
            foreach (var target in targetNames)
            {
                reader.Read();//target root class
                var serializer = new XmlSerializer(typeof(SerialazibleDictionary<string, LogLevel>), new XmlRootAttribute(reader.Name));

                var dict = (SerialazibleDictionary<string, LogLevel>)serializer.Deserialize(reader);

                baseToRead.Add(target, dict);
            }

            reader.Read();//last target closing tag

            var s = new XmlSerializer(typeof(LogLevel), new XmlRootAttribute(reader.Name));

            defaultLevel = (LogLevel) s.Deserialize(reader);

            return baseToRead;
        }

        public Dictionary<string, SerialazibleDictionary<string, Writer>> ReadWriterBase(out Writer defaultWriter)
        {
            //TODO: here can be exceptions
            
            string[] targetNames = { "classes", "assemblies" };

            var baseToRead = new Dictionary<string, SerialazibleDictionary<string, Writer>>();
            
            var readerSettings = new XmlReaderSettings { IgnoreWhitespace = true };

            var reader = XmlReader.Create(_fileName, readerSettings);

            reader.Read();//header
            reader.Read();//document root
            
            foreach (var target in targetNames)
            {
                reader.Read();//target root class
                var serializer = new XmlSerializer(typeof(SerialazibleDictionary<string, Writer>), new XmlRootAttribute(reader.Name));

                var dict = (SerialazibleDictionary<string, Writer>)serializer.Deserialize(reader);

                baseToRead.Add(target, dict);
            }
            
            reader.Read();//last target closing tag
            reader.Read();//"Writer" tag

            var s = new XmlSerializer(typeof(Writer));

            defaultWriter = (Writer)s.Deserialize(reader);

            return baseToRead;
        }

        private readonly string _fileName;
    }
}