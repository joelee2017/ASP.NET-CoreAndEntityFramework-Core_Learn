using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Mapper;
using StudentManagement.Model;
using StudentManagement.ViewModels;
using StudentManagementDataAccess.Models;
using StudentManagementDataAccess.Repository;
using System;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository _studentRepository;
        private ILogger<HomeController> logger;

        //使用建構式注入的方式注入IStudentRepository
        public HomeController(IStudentRepository studentRepository,ILogger<HomeController> logger)
        {
            _studentRepository = studentRepository;
            this.logger = logger;
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
            //logger.LogTrace("Trace(跟踪) Log");
            //logger.LogDebug("Debug(偵錯) Log");
            //logger.LogInformation("信息(Information) Log");
            //logger.LogWarning("警告(Warning) Log");
            //logger.LogError("錯誤(Error) Log");
            //logger.LogCritical("嚴重(Critical) Log");

            //throw new Exception("在Details中異常");
            var _student = _studentRepository.GetStudent(id.Value).Map<Student, StudentViewModel>();
            if (_student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }

            //實體化HomeDetailsViewModel並存儲Student詳细信息和PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = _student,
                PageTitle = "Student Details"
            };

            //將ViewModel對象傳给View()方法
            return View(homeDetailsViewModel);
        }

        // /home/DetailsObject
        public ObjectResult DetailsObject()
        {
            StudentViewModel model = _studentRepository.GetStudent(1).Map<Student, StudentViewModel>();
            return new ObjectResult(model);

        }

        // /home/DetailsView
        public ViewResult DetailsView()
        {
            StudentViewModel model = _studentRepository.GetStudent(1).Map<Student, StudentViewModel>();
            // 相對路徑
            //  return View("../Test/MyViews/Details");

            // 絕對路徑
            return View("MyViews/Details.cshtml");
        }

        // /home/DetailsViewData
        public ViewResult DetailsViewData()
        {
            StudentViewModel model = _studentRepository.GetStudent(1).Map<Student, StudentViewModel>();
            //使用ViewData將PageTitle和Student模型傳给View
            ViewData["PageTitle"] = "Student DetailsViewData";
            ViewData["Student"] = model;

            return View();
        }

        // /home/DetailsViewBag
        public ViewResult DetailsViewBag()
        {
            StudentViewModel model = _studentRepository.GetStudent(1).Map<Student, StudentViewModel>();
            //將PageTitle和Student模型對象存儲在ViewBag
            //我們正在使用動態屬性PageTitle和Student
            ViewBag.PageTitle = "Student DetailsViewBag";
            ViewBag.Student = model;

            return View();
        }

        // /home/DetailsModel
        public ViewResult DetailsModel()
        {
            StudentViewModel model = _studentRepository.GetStudent(1).Map<Student, StudentViewModel>();

            ViewBag.PageTitle = "Student DetailsModel";

            return View(model);
        }

        // /home/DetailsViewModel
        public ViewResult DetailsViewModel()
        {
            //實體化HomeDetailsViewModel并存儲Student詳细信息和PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(1).Map<Student, StudentViewModel>(),
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
        public IActionResult Create(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                StudentViewModel newStudent = _studentRepository.Add(student.Map<StudentViewModel, Student>()).Map<Student, StudentViewModel>();
                return RedirectToAction("Details", new { id = newStudent.Id });
            }

            return View(student);
        }
    }
}

