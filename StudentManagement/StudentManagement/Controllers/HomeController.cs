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

        public JsonResult Details()
        {
            Student model = _studentRepository.GetStudent(1);
            return Json(model);
        }

        public ObjectResult DetailsObject()
        {
            Student model = _studentRepository.GetStudent(1);
            return new ObjectResult(model);

        }

        public ViewResult DetailsView()
        {
            Student model = _studentRepository.GetStudent(1);
            // 相對路徑
            //  return View("../Test/MyViews/Details");

            // 絕對路徑
            return View("MyViews/Details.cshtml");
        }
    }
}
