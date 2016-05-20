using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public class XmlLevelBaseReader
    {
        public static void Write(string fileName, SerialazibleDictionary<string, SerialazibleDictionary<string, Level>> baseToWrite)
        {
            var stream = new FileStream(fileName, FileMode.Append);

            foreach (var target in baseToWrite)
            {
                var rootName = new XmlRootAttribute(target.Key);
                var serializer = new XmlSerializer(typeof(SerialazibleDictionary<string, Level>), rootName);
                target.Value.RootName = target.Key;
                serializer.Serialize(stream, target.Value);
            }

            stream.Dispose();
        }


        public static SerialazibleDictionary<string, SerialazibleDictionary<string, Level>> Read(string fileName)
        {
            var baseToRead = new SerialazibleDictionary<string, SerialazibleDictionary<string, Level>>();

            var readerSettings = new XmlReaderSettings {IgnoreWhitespace = true};
            var reader = XmlReader.Create(fileName, readerSettings);
            reader.Read();
            
            while (reader.Read() && reader.Name != "LevelSettings")
            {
                var name = reader.Name;
                var rootName = new XmlRootAttribute(name);
                var serializer = new XmlSerializer(typeof(SerialazibleDictionary<string, Level>), rootName);

                var dict = (SerialazibleDictionary<string, Level>)serializer.Deserialize(reader);

                baseToRead.Add(name, dict);
            }

            return baseToRead;
        }
    }
}