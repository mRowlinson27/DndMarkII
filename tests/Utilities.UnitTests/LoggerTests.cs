
namespace Utilities.UnitTests
{
    using System;
    using System.Text.RegularExpressions;
    using API;
    using API.DAL;
    using FakeItEasy;
    using Implementation;
    using NUnit.Framework;

    [TestFixture]
    public class LoggerTests
    {
        private Logger _logger;
        private IFileWriter _fileWriter;
        private IDateTimeWrapper _dateTimeWrapper;

        [SetUp]
        public void Setup()
        {
            _fileWriter = A.Fake<IFileWriter>();
            _dateTimeWrapper = A.Fake<IDateTimeWrapper>();
            _logger = new Logger(_fileWriter, _dateTimeWrapper);
        }

        [Test]
        public void LogMessage_LogsLineNumberOfCaller()
        {
            //Arrange
            var message = "message";
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);

            var correctsourceLineNumber = "1234";

            //Act
            _logger.LogMessage(message, sourceLineNumber: 1234);

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => str.Contains(correctsourceLineNumber)))).MustHaveHappened();
        }

        [Test]
        public void LogMessage_LogsTimeWhenCalled()
        {
            //Arrange
            var message = "message";
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);

            var correctTimeSection = "03:04:05:000000 AM | ";

            //Act
            _logger.LogMessage(message);

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => str.Contains(correctTimeSection)))).MustHaveHappened();
        }

        [Test]
        public void LogMessage_LogsMethodNameOfCaller()
        {
            //Arrange
            var message = "message";
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);

            var correctMethodSection = "LoggerTests::LogMessage_LogsMethodNameOfCaller:";

            //Act
            _logger.LogMessage(message, memberName: "LogMessage_LogsMethodNameOfCaller");

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => str.Contains(correctMethodSection)))).MustHaveHappened();
        }

        [Test]
        public void LogMessage_LogsMessage()
        {
            //Arrange
            var message = "message";
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);
            
            var correctMessageSection = "message";

            //Act
            _logger.LogMessage(message);

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => str.Contains(correctMessageSection)))).MustHaveHappened();
        }

        [Test]
        public void LogMessage_LogThreadId()
        {
            //Arrange
            var message = "message";
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);
            var regexPattern = "\\| \\d+ \\|";

            //Act
            _logger.LogMessage(message);

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => Regex.IsMatch(str, regexPattern)))).MustHaveHappened();
        }

        [Test]
        public void LogEntry_LogsEntryMessage()
        {
            //Arrange
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);
            var correctMessageSection = "ENTERING";

            //Act
            _logger.LogEntry();

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => str.Contains(correctMessageSection)))).MustHaveHappened();
        }

        [Test]
        public void LogExit_LogsExitMessage()
        {
            //Arrange
            var currentTime = new DateTime(2000, 1, 2, 3, 4, 5);
            A.CallTo(() => _dateTimeWrapper.GetCurrentDateTime()).Returns(currentTime);
            var correctMessageSection = "EXITING";

            //Act
            _logger.LogExit();

            //Assert
            A.CallTo(() => _fileWriter.Write(A<string>.Ignored, A<string>.That.Matches(str => str.Contains(correctMessageSection)))).MustHaveHappened();
        }
    }
}
