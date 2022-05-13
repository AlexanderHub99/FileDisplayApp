using FileDisplayApp.interfaces;
using FileDisplayApp.Models;

namespace FileDisplayApp.Script
{
    public  class DirectorysInfo:IParser
    {
        public List<MyFiles> ParserInfo(string directory)
        {
            List<MyFiles> listFiles = new List<MyFiles>();

           // System.IO.DirectoryInfo di ;
            List<string> dirs;

            try
            {
                dirs = new List<string>(Directory.EnumerateDirectories(directory));
            }
            catch (Exception e) // Можно обработать ошибку и куда-то вывести 
            {
                dirs = new List<string>(Directory.EnumerateDirectories("I:\\")); //Заглушка
            }

            foreach (string directoryInfo in dirs)
            {
                double catalogSize = 0;
                string[] failName = directoryInfo.Split("\\").ToArray();
                MyFiles myFiles = new MyFiles
                {
                    Directoire = directoryInfo,
                    Name = failName[^1],//Синтаксический сахор молжно заменить на [failName.Length - 1]
                    Size = SizeOfFolder(directoryInfo, ref catalogSize)
                };

                listFiles.Add(myFiles);
            }

            return listFiles;
        }
        private double SizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diInfo = di.GetDirectories();
                System.IO.FileInfo[] fi = di.GetFiles();
                
                foreach ( System.IO.FileInfo f in fi) //В цикле пробегаемся по всем файлам директории di и складываем их размеры
                {
                    //В переменную catalogSize будем записывать размеры всех файлов, с каждым
                    //новым файлом перезаписывая данную переменную
                    catalogSize = catalogSize + f.Length; //Записываем размер файла в байтах
                }
                //В цикле пробегаемся по всем вложенным директориям директории di 
                foreach (DirectoryInfo dInfo in diInfo)
                {
                    SizeOfFolder(dInfo.FullName, ref catalogSize); //рекурсивно вызываем наш метод пока не закончатся
                                                                   //деректории по которым можем прыгать 
                }
                return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);//Спасибо Google крутая штука однако.
            }
            catch (DirectoryNotFoundException ex) //директория не найдена
            {
                return 0;
            }
            catch (UnauthorizedAccessException ex) //отсутствует доступ к файлу или папке
            {
                return 0;
            }
            catch (Exception ex) //Во всех остальных случаях
            {
                return 0;
            }
        }
    }
}

