
namespace Utilities.Implementation
{
    using System;
    using System.Threading;
    using API;
    using API.DAL;

    public class Logger : ILogger
    {
        private readonly IFileWriter _fileWriter;

        private readonly IDateTimeWrapper _dateTimeWrapper;

        private string logFilePath = "C:\\Temp\\CharacterLog.txt";

        private object sync = new object();

        private const string EntryMessage = "ENTERING";

        private const string ExitMessage = "EXITING";

        public Logger(IFileWriter fileWriter, IDateTimeWrapper dateTimeWrapper)
        {
            _fileWriter = fileWriter;
            _dateTimeWrapper = dateTimeWrapper;
        }

        public void LogMessage(string message, 
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var time = _dateTimeWrapper.GetCurrentDateTime();
            var classFileName = sourceFilePath.Substring(sourceFilePath.LastIndexOf('\\') + 1);
            var className = classFileName.Substring(0, classFileName.Length - 3);

            var finalLog = time.ToString("hh:mm:ss:ffffff tt") + " | " + Thread.CurrentThread.ManagedThreadId + " | " + className + "::" + memberName + ":" + sourceLineNumber +
                           " | " + message + '\n';

            lock (sync)
            {
                _fileWriter.Write(logFilePath, finalLog);
            }

#if DEBUG
            Console.Write(finalLog);
#endif
        }

        public void LogEntry([System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            LogMessage(EntryMessage, memberName, sourceFilePath, sourceLineNumber);
        }

        public void LogExit([System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            LogMessage(ExitMessage, memberName, sourceFilePath, sourceLineNumber);
        }
    }
}
