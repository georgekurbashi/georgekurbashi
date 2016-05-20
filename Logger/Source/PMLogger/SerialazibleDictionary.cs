using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace PMLogger
{
    public class SerialazibleDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerialazibleDictionary()
        {
 
        }
 
        public SerialazibleDictionary(IDictionary<TKey, TValue> other)
            : base(other)
        {
 
        }
        
        public SerialazibleDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
 
        }
        
        public SerialazibleDictionary(int capacity)
            : base(capacity)
        {
 
        }

        public SerialazibleDictionary(IDictionary<TKey, TValue> other, IEqualityComparer<TKey> comparer)
            : base(other, comparer)
        {
 
        }
        
        public SerialazibleDictionary(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
 
        }
        
        protected SerialazibleDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
 
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader baseReader)
        {
            var keyDeserializer = new XmlSerializer(typeof (TKey));
            var valueDeserializer = new XmlSerializer(typeof(TValue));
            RootName = baseReader.Name;
            while (baseReader.Read())
            {
                if (baseReader.NodeType == XmlNodeType.EndElement && baseReader.Name == "item")
                    baseReader.Read();

                if (baseReader.NodeType == XmlNodeType.EndElement && baseReader.Name == RootName)  
                    break;

                if (baseReader.Name != "item")
                    throw new XmlException("Invalid XML");

                //reading key
                baseReader.Read();

                if (baseReader.Name != "key")
                    throw new XmlException("Invalid XML");
                
                baseReader.Read();
                
                var key = (TKey)keyDeserializer.Deserialize(baseReader);
                
                //reading value
                baseReader.Read();
                
                if (baseReader.Name != "value")
                    throw new XmlException("Invalid XML");

                baseReader.Read();
                
                var value = (TValue)valueDeserializer.Deserialize(baseReader);
                
                
                Add(key, value);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerialier = new XmlSerializer(typeof(TValue));
            
            foreach (var pair in this)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, pair.Key);
                writer.WriteEndElement();
                
                writer.WriteStartElement("value");
                valueSerialier.Serialize(writer, pair.Value);
                writer.WriteEndElement();
                
                writer.WriteEndElement();
            }
        }

        public string RootName { get; set; }
    }
}