using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PMLogger
{
    public class LevelBase
    {
        public LevelBase(string fileName = null)
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

            var configXml = XmlReader.Create(configFileStream, new XmlReaderSettings {IgnoreWhitespace = true});
            
            configXml.ReadToFollowing("level_base_file");
            configXml.Read();

            return configXml.Value;
        }

        private void SetDefault()
        {
            _levelBase = new Dictionary<string, SerialazibleDictionary<string, LogLevel>>();
            _levelBase["classes"] = new SerialazibleDictionary<string, LogLevel>();
            _levelBase["assemblies"] = new SerialazibleDictionary<string, LogLevel>();

            DefaultLogLevel = LogLevel.Debug;
        }

        private void LoadFromFile(string fileName)
        {
            var reader = new XmlBaseReader(fileName);

            try
            {
                LogLevel defaultLogLevel;
                _levelBase = reader.ReadLevelBase(out defaultLogLevel);
                DefaultLogLevel = defaultLogLevel;
            }
            catch (Exception)
            {
                SetDefault();
            }
        }

        public void SetLevel(LogLevel level)
        {
            var caller = new Caller(2);

            _levelBase["classes"][caller.ClassName] = level;
        }

        public void SetClassLevel(string className, LogLevel level)
        {
            _levelBase["classes"][className] = level;
        }

        public void SetAssemblyLevel(string assemblyName, LogLevel level)
        {
            _levelBase["assemblies"][assemblyName] = level;
        }

        public LogLevel GetLevel(Caller caller)
        {
            if (_levelBase["classes"].ContainsKey(caller.ClassName))
                return _levelBase["classes"][caller.ClassName];

            if (_levelBase["assemblies"].ContainsKey(caller.AssemblyName))
                return _levelBase["assemblies"][caller.AssemblyName];

            return DefaultLogLevel;
        }
        
        private Dictionary<string, SerialazibleDictionary<string, LogLevel>> _levelBase;

        private LogLevel DefaultLogLevel { get; set; }
    }
}