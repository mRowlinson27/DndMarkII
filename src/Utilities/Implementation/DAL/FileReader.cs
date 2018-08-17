
namespace Utilities.Implementation.DAL
{
    using System.IO;
    using API.DAL;

    public class FileReader : IFileReader
    {
        public string ReadFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }
    }
}