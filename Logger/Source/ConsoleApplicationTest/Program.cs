using System;
using PMLogger;
using PMLogger.Formatters;

namespace ConsoleApplicationTest
{
    class Program
    {
        private static void Main(string[] args)
        {
            var levelName = "C:/Users/Андрей/Desktop/Учеба/Методы программирования/Logger/Build/AnyCPU/Debug/Lib/Logger/level.xml";
            var baseName = "C:/Users/Андрей/Desktop/Учеба/Методы программирования/Logger/Build/AnyCPU/Debug/Lib/Logger/writer.xml";

            var logger = new Logger(levelName, baseName);

            logger.LogDebug("ololo");
            logger.LogDebug("ololo");

            logger.LevelBase.SetLevel(LogLevel.Off);

            logger.LogDebug("ololo");
           
        }
    }
}