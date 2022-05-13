using FileDisplayApp.interfaces;
using FileDisplayApp.Models;
using FileDisplayApp.Script;
using FileInfo = FileDisplayApp.Script.FileInfo;

namespace FileDisplayApp.Context
{
    public class ContextFiles
    {
        private readonly List<IParser> _parsers = new List<IParser>();

        public List<MyFiles> File { get; private set; }
        
        public ContextFiles()
        {
            AddService(new DirectorysInfo());
            AddService(new FileInfo());
            File = InitDirectoryFileInfo();
        }

        public List<MyFiles> SetFile(IOrderedEnumerable<MyFiles>  Files)
        {
            File = new List<MyFiles>(Files);
            return File;
        }
        public void SetDirectoryFile(string @string)
        {
            File = InitDirectoryFileInfo(@string);
        }
        
        private List<MyFiles> InitDirectoryFileInfo(string @string = null)
        {
            if (@string == null)
            {
                @string = "I:\\";
            }

            List<MyFiles> myFiles = new List<MyFiles>();

            foreach (IParser parser  in _parsers)
            {
                List<MyFiles> info = parser.ParserInfo(@string);
                myFiles.AddRange(info);
            }

            return myFiles;
        }
        private void AddService(IParser service)
        {
            _parsers.Add(service);
        }
    }
}