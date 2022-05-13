using FileDisplayApp.interfaces;
using FileDisplayApp.Models;

namespace FileDisplayApp.Script
{
    public  class FileInfo:IParser
    {
        public List<MyFiles> ParserInfo(string directory)
        {
            List<MyFiles> listFiles = new List<MyFiles>();

            System.IO.DirectoryInfo di;
            System.IO.FileInfo[] fileInfos;

            try
            {
                di= new  System.IO.DirectoryInfo(directory); // Сделать ссылку на каталог.
                fileInfos = di.GetFiles();                   // Получить ссылку на каждый файл в этом каталоге.
            }
            catch (Exception e) // Можно обработать ошибку и куда-то вывести 
            {
                di = new System.IO.DirectoryInfo("I:\\"); //Заглушка
                fileInfos = di.GetFiles();                // Получить ссылку на каждый файл в этом каталоге.
            }
            
            if (di.Exists) // Можно обработать ошибку и куда-то вывести 
            {
                di = new System.IO.DirectoryInfo("I:\\"); //Заглушка
                fileInfos = di.GetFiles();                // Получить ссылку на каждый файл в этом каталоге.
            }

            foreach (System.IO.FileInfo fileInfo in fileInfos)
            {
                MyFiles myFiles = new MyFiles();
                myFiles.Directoire = Convert.ToString(fileInfo.Directory);
                myFiles.Name = fileInfo.Name;
                myFiles.Size = Math.Round((double)(fileInfo.Length / 1024 / 1024), 1);
                listFiles.Add(myFiles);
            }

            return listFiles;
        }
    }
}

