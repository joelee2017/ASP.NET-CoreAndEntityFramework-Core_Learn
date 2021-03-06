using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello from MVC";
        }
    }
}
