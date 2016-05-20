using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using PMLogger;
using PMLogger.Formatters;
using File = PMLogger.OutputDevices.File;

namespace Tests
{
    [TestFixture]
    public class FormatterTests
    {
        [Test]
        public void DateTest()
        {
            IFormatter date = new AddDate();
            
            var expected = string.Format("[{0}.{1}.{2}] {3}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "Ololo");
            Assert.That(date.Format("Ololo", LogLevel.Off), Is.EqualTo(expected));

            IFormatter date2 = new AddDate(date);

            var expected2 = string.Format("[{0}.{1}.{2}] {3}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, expected);
            Assert.That(date2.Format("Ololo", LogLevel.Off), Is.EqualTo(expected2));
        }

        [Test]
        public void TimeTest()
        {
            IFormatter time = new AddTime();

            IFormatter time2 = new AddTime(time);

            var actual = time2.Format("Ololo", LogLevel.Off);
            Assert.That(actual, Is.StringMatching(@"^[[](\d)+\:(\d)+\:(\d)+\.(\d)+[]].*"));
        }

        [Test]
        public void MethodTest()
        {
            IFormatter level = new AddMethodName();

            IFormatter methodName = new AddMethodName(level);

            var caller = new Caller(4);

            var expected = string.Format("[Method : {0}] [Method : {1}] {2}", caller.MethodName.Name, "UnsafeInvokeInternal", "Ololo");
            Assert.That(methodName.Format("Ololo", LogLevel.Off), Is.EqualTo(expected));   
        }

        [Test]
        public void LevelTest()
        {
            IFormatter level = new AddLevel();
            
            var expected = string.Format("[Level: {0}] {1}", LogLevel.Debug, "Ololo");
            Assert.That(level.Format("Ololo", LogLevel.Debug), Is.EqualTo(expected));

            IFormatter level2 = new AddLevel(level);

            var expected2 = string.Format("[Level: {0}] {1}", LogLevel.Debug, expected);
            Assert.That(level2.Format("Ololo", LogLevel.Debug), Is.EqualTo(expected2));

        }

        [Test]
        public void ProcessIdTest()
        {
            IFormatter processId = new AddProcessId();

            IFormatter processId2 = new AddProcessId(processId);

            var pid = Process.GetCurrentProcess().Id;

            var actual = processId2.Format("Ololo", LogLevel.Off);
            Assert.That(actual, Is.EqualTo(string.Format("[ProcessID = {0}] [ProcessID = {0}] {1}", pid, "Ololo")));
        }

        [Test]
        public void ThreadIdTest()
        {
            IFormatter threadId = new AddThreadId();

            IFormatter threadId2 = new AddThreadId(threadId);

            var pid = Thread.CurrentThread.ManagedThreadId;

            var actual = threadId2.Format("Ololo", LogLevel.Off);
            Assert.That(actual, Is.EqualTo(string.Format("[ThreadID = {0}] [ThreadID = {0}] {1}", pid, "Ololo")));
        }
    }



    [TestFixture]
    public class DifferentTests
    {
        [Test]
        public void CallerTest()
        {
            var caller = new Caller(1);

            Assert.That(caller.ClassName, Is.EqualTo("Tests.DifferentTests"));
            Assert.That(caller.AssemblyName, Is.EqualTo("Tests"));
            Assert.That(caller.MethodName.Name, Is.EqualTo("CallerTest"));

        }

        [Test]
        public void FileWriterTest()
        {
            var fileWriter = new File("testFile.log");
            var fileWriter2 = new File("testFile.log", fileWriter);

            fileWriter2.Write("Hello, World !");

            var expectedFile = new StreamWriter("expected.txt", false);
            expectedFile.WriteLine("Hello, World !");
            expectedFile.WriteLine("Hello, World !");
            expectedFile.Close();
            
            FileAssert.AreEqual("expected.txt", "testFile.log");

            System.IO.File.Delete("testFile.log");
            System.IO.File.Delete("expected.txt");
        }

        [Test]
        public void WriterTest()
        {
            var fileWriter = new File("testFile.log");
            var fileWriter2 = new File("testFile.log", fileWriter);

            var writer = new Writer(new AddMethodName(), fileWriter2);

            writer.Write("Hello, World !", LogLevel.Debug);

            var expectedFile = new StreamWriter("expected.txt", false);
            expectedFile.WriteLine("[Method : UnsafeInvokeInternal] Hello, World !");
            expectedFile.WriteLine("[Method : UnsafeInvokeInternal] Hello, World !");
            expectedFile.Close();

            FileAssert.AreEqual("expected.txt", "testFile.log");

            System.IO.File.Delete("testFile.log");
            System.IO.File.Delete("expected.txt");
        }
    }



    [TestFixture]
    public class LevelBaseTests
    {
        [Test]
        public void ConstructorWithoutConfigTest()
        {
            System.IO.File.Move("./Lib/Logger/config.xml", "./Lib/Logger/configx.xml");
            Assert.That(() => { var newBase = new LevelBase(); }, Throws.Exception);
            System.IO.File.Move("./Lib/Logger/configx.xml", "./Lib/Logger/config.xml");
        }

        [Test]
        public void ConstructorWithConfigTest()
        {
            var newBase = new LevelBase();
        }
    }


    [TestFixture]
    public class WriterBaseTests
    {
        [Test]
        public void ConstructorWithoutConfigTest()
        {
            System.IO.File.Move("./Lib/Logger/config.xml", "./Lib/Logger/configx.xml");
            Assert.That(() => { var newBase = new WriterBase(); }, Throws.Exception);
            System.IO.File.Move("./Lib/Logger/configx.xml", "./Lib/Logger/config.xml");
        }

        [Test]
        public void ConstructorWithConfigTest()
        {
            var newBase = new WriterBase();
        }
    }
}