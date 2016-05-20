using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PMLogger.Formatters;
using Console = PMLogger.OutputDevices.Console;

namespace PMLogger
{
    public class WriterBase
    {
        public WriterBase(string fileName = null)
        {
            if (fileName == null)
                fileName = ReadFileNameFromConfigFile();

            if (fileName == null)
            {
                SetDefault();
                return;
            }

            LoadFromFile(fileName);
        }

        private void SetDefault()
        {
            _writerBase = new Dictionary<string, SerialazibleDictionary<string, Writer>>();
            _writerBase["classes"] = new SerialazibleDictionary<string, Writer>();
            _writerBase["assemblies"] = new SerialazibleDictionary<string, Writer>();

            DefaultWriter = new Writer(new AddDate(new AddTime(new AddMethodName())), new Console());
        }

        private static string ReadFileNameFromConfigFile()
        {
            FileStream configFileStream;

            try
            {
                configFileStream = new FileStream("./Lib/Logger/config.xml", FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Config file missing !");
            }

            var configXml = XmlReader.Create(configFileStream, new XmlReaderSettings { IgnoreWhitespace = true });

            configXml.ReadToFollowing("writer_base_file");
            configXml.Read();

            return configXml.Value;
        }

        private void LoadFromFile(string fileName)
        {
            var reader = new XmlBaseReader(fileName);

            try
            {
                Writer defaultLogLevel;
                _writerBase = reader.ReadWriterBase(out defaultLogLevel);
                DefaultWriter = defaultLogLevel;
            }
            catch (Exception)
            {
                SetDefault();
            }
        }

        public void SetLevel(Writer writer)
        {
            var caller = new Caller(2);

            _writerBase["classes"][caller.ClassName] = writer;
        }

        public void SetClassLevel(string className, Writer writer)
        {
            _writerBase["classes"][className] = writer;
        }

        public void SetAssemblyLevel(string assemblyName, Writer writer)
        {
            _writerBase["assemblies"][assemblyName] = writer;
        }

        public Writer GetWriter(Caller caller)
        {
            if (_writerBase["classes"].ContainsKey(caller.ClassName))
                return _writerBase["classes"][caller.ClassName];

            if (_writerBase["assemblies"].ContainsKey(caller.AssemblyName))
                return _writerBase["assemblies"][caller.AssemblyName];

            return DefaultWriter;
        }

        private Dictionary<string, SerialazibleDictionary<string, Writer>> _writerBase;

        private Writer DefaultWriter { get; set; }
    }
}