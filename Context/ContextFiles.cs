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

        public List<MyFiles> SetFile(IOrderedEnumerable<MyFiles>  files)
        {
            File = new List<MyFiles>(files);
            return File;
        }
        public void SetDirectoryFile(string linkDirectory)
        {
            File = InitDirectoryFileInfo(linkDirectory);
        }
        
        private List<MyFiles> InitDirectoryFileInfo(string linkDirectory = null!)
        {
            if (linkDirectory == null)
            {
                linkDirectory = "C:\\";
            }

            List<MyFiles> myFiles = new List<MyFiles>();

            foreach (IParser parser  in _parsers)
            {
                List<MyFiles> info = parser.ParserInfo(linkDirectory);
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