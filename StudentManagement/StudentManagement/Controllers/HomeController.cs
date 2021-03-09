using Microsoft.AspNetCore.Mvc;
using StudentManagement.Model;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        //public JsonResult Index()
        //{
        //    return Json(new { id = 1, name = "pragim" });
        //}

        private IStudentRepository _studentRepository;

        //使用建構式注入的方式注入IStudentRepository
        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        //返回学生的名字
        public string Index()
        {
            return _studentRepository.GetStudent(1).Name;
        }

        // /home/Details
        public JsonResult Details()
        {
            Student model = _studentRepository.GetStudent(1);
            return Json(model);
        }

        // /home/DetailsObject
        public ObjectResult DetailsObject()
        {
            Student model = _studentRepository.GetStudent(1);
            return new ObjectResult(model);

        }

        // /home/DetailsView
        public ViewResult DetailsView()
        {
            Student model = _studentRepository.GetStudent(1);
            // 相對路徑
            //  return View("../Test/MyViews/Details");

            // 絕對路徑
            return View("MyViews/Details.cshtml");
        }

        // /home/DetailsViewData
        public ViewResult DetailsViewData()
        {
            Student model = _studentRepository.GetStudent(1);
            //使用ViewData將PageTitle和Student模型傳给View
            ViewData["PageTitle"] = "Student Details";
            ViewData["Student"] = model;

            return View();
        }

        // /home/DetailsViewBag
        public ViewResult DetailsViewBag()
        {
            Student model = _studentRepository.GetStudent(1);
            //將PageTitle和Student模型對象存儲在ViewBag
            //我們正在使用動態屬性PageTitle和Student
            ViewBag.PageTitle = "Student Details";
            ViewBag.Student = model;

            return View();
        }

        // /home/DetailsModel
        public ViewResult DetailsModel()
        {
            Student model = _studentRepository.GetStudent(1);

            ViewBag.PageTitle = "Student Details";

            return View(model);
        }
    }
}
