using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class ErrorController : Controller
    {
        //如果狀態代碼為404，財路徑將變為Error/404
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你訪問的頁面不存在";
                    break;
            }

            return View("NotFound");
        }
    }
}
