using Microsoft.AspNetCore.Mvc;
using StudentManagement.Model;
using StudentManagement.ViewModels;

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

        public ViewResult IndexRoute()
        {
            ViewData["PageTitle"] = "Student IndexRoute";
            return View();
        }

        //返回學生的名字
        //public string Index()
        //{
        //    return _studentRepository.GetStudent(1).Name;
        //}

        //返回學生的名字
        //  /home
        public ViewResult Index()
        {
            //查詢所有的學生信息
            var model = _studentRepository.GetAllStudents();
            //將學生列表傳到視圖
            return View(model);
        }

        // /home/Details
        //public JsonResult Details()
        //{
        //    Student model = _studentRepository.GetStudent(1);
        //    return Json(model);
        //}

        public ViewResult Details(int? id)
        {
            id = id.Value < 1 ? 1 : id.Value;
            id = id.Value > 3 ? 1 : id.Value;
            //實體化HomeDetailsViewModel並存儲Student詳细信息和PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {               
                Student = _studentRepository.GetStudent(id ?? 1),
                PageTitle = "Student Details"
            };

            //將ViewModel對象傳给View()方法
            return View(homeDetailsViewModel);
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
            ViewData["PageTitle"] = "Student DetailsViewData";
            ViewData["Student"] = model;

            return View();
        }

        // /home/DetailsViewBag
        public ViewResult DetailsViewBag()
        {
            Student model = _studentRepository.GetStudent(1);
            //將PageTitle和Student模型對象存儲在ViewBag
            //我們正在使用動態屬性PageTitle和Student
            ViewBag.PageTitle = "Student DetailsViewBag";
            ViewBag.Student = model;

            return View();
        }

        // /home/DetailsModel
        public ViewResult DetailsModel()
        {
            Student model = _studentRepository.GetStudent(1);

            ViewBag.PageTitle = "Student DetailsModel";

            return View(model);
        }

        // /home/DetailsViewModel
        public ViewResult DetailsViewModel()
        {
            //實體化HomeDetailsViewModel并存儲Student詳细信息和PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(1),
                PageTitle = "Student DetailsViewModel"
            };

            //將ViewModel傳给View()方法
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(Student student)
        {
            Student newStudent = _studentRepository.Add(student);
            return RedirectToAction("Details", new { id = newStudent.Id });
        }
    }
}

