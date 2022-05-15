using FileDisplayApp.Context;
using FileDisplayApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace FileDisplayApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextFiles _context;

        public HomeController(ContextFiles context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index(string dl)
        {
            if (dl == "true")
            {
                IOrderedEnumerable<MyFiles> sortedPeople1 = from files in _context.File
                    orderby files.Size descending // сортировка по возрасту по убыванию
                    select files;
                ViewBag.FileDirectory = _context.SetFile(sortedPeople1) ;
            }else if(dl == "false")
            {
                IOrderedEnumerable<MyFiles> sortedPeople1 = from files in _context.File
                    orderby files.Size // сортировка по возрасту по убыванию
                    select files;
                ViewBag.FileDirectory = _context.SetFile(sortedPeople1) ;
            }else if (dl == null)
            {
                ViewBag.FileDirectory = _context.File;

                return View();
            }else {
                
                _context.SetDirectoryFile(dl);
                ViewBag.FileDirectory = _context.File;
                
                return View();
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(MyFiles myFiles)
        {
            if (myFiles.Directoire != null)
            {
                _context.SetDirectoryFile(myFiles.Directoire);
            }

            ViewBag.FileDirectory = _context.File;

            return View();
        }
    }
}

