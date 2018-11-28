using System;
using NUnit.Framework;
using static System.String;

namespace Epam.JDI.Core.Logging
{
    public class NUnitLogger : ILogger
    {
        private readonly bool _writeDebug;

        public NUnitLogger(bool writeDebug = true)
        {
            _writeDebug = writeDebug;
        }

        private static string Dt => $"{DateTime.Now:hh:mm:ss dd.MM.yy}";
        public static string LogTemplate => "[{0}: {1}] {2}";

        public void Exception(Exception ex)
        {
            TestContext.WriteLine($"Exception");
        }

        public void Trace(string message)
        {
            TestContext.WriteLine(Format(LogTemplate, "Trace", Dt, message));
            TestContext.WriteLine($"[Trace: {Dt}] {message}");
            System.Diagnostics.Debug.WriteLine("");
        }

        public void Debug(string message)
        {
            if (_writeDebug)
            {
                TestContext.WriteLine(Format(LogTemplate, "Debug", Dt, message));
            }
        }

        public void Info(string message)
        {
            TestContext.WriteLine(Format(LogTemplate, "Info", Dt, message));
        }

        public void Error(string message)
        {
            TestContext.WriteLine(Format(LogTemplate, "Error", Dt, message));
        }

        public void Step(string message)
        {
            throw new NotImplementedException();
        }

        public void TestDescription(string message)
        {
            throw new NotImplementedException();
        }

        public void TestSuit(string message)
        {
            throw new NotImplementedException();
        }
    }
}
