using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StudentManagement.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> logger;


        ///<summary>
        ///注入ASP.NET  Core ILogger服務。
        ///將控制器類型指定为泛型参數。
        ///這有助于我们進行確定哪個類或控制器產生了異常，然后記錄它
        ///</summary>
        ///<param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //獲取異常詳情信息
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //LogError() 方法將異常記錄作為日誌中的錯誤類別記錄
            logger.LogError($"路徑 {exceptionHandlerPathFeature.Path} " +
                $"產生了一个錯誤{exceptionHandlerPathFeature.Error}");


            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            return View("Error");
        }

        // 測試/market/food/3?name=apple
        //如果狀態代碼為404，則路徑將變為Error/404
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {

            var statusCodeResult =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你訪問的頁面不存在";
                    //LogWarning() 方法將異常記錄作為日誌中的警告類別記錄
                    logger.LogWarning($"發生了一個404錯誤. 路徑 = " +
                $"{statusCodeResult.OriginalPath} 以及查詢字符串 = " +
                $"{statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
    }
}
