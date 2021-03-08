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
    }
}
