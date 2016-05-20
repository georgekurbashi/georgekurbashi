using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PMLogger
{
    public class Caller
    {
        public MethodBase MethodName { get; set; }
        public string ClassName { get; set; }
        public string AssemblyName { get; set; }

        public Caller(int frameNumber = 1)
        {
            var frame = new StackTrace().GetFrame(frameNumber);

            var assemblyInfo = frame.GetMethod().ReflectedType.Assembly.GetName().FullName;

            MethodName = frame.GetMethod();

            ClassName = frame.GetMethod().DeclaringType.FullName;

            AssemblyName = Regex.Match(assemblyInfo, "(.*?)(?<!\\\\)\\,").Groups[1].Value;
        }
    }
}
