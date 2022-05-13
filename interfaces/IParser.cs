using FileDisplayApp.Models;

namespace FileDisplayApp.interfaces
{
    public interface IParser
    {
        public List<MyFiles> ParserInfo(string directory);
    }
}

