using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace StudentManagement
{
    public class Startup
    {
        private IConfiguration _configuration;

        // 加入建構式依賴注入
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount = 10
                //};
                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);
                app.UseDeveloperExceptionPage();
            }

            //有點類似setting的感覺,在這個middleware會針對request的內容去做初始化
            app.UseRouting();

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("abc.html");

            //// 添加默認文件
            //app.UseDefaultFiles(defaultFilesOptions);

            //// 添加靜態文件中間件
            app.UseStaticFiles();


            // 使用UseFileServer而不是UseDefaultFiles和UseStaticFiles
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("default.html");
            //app.UseFileServer(fileServerOptions);


            //這邊才是真正去設定的地方,使用delegate去做設定
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context => 
            //    {
            //        //避免亂碼
            //        context.Response.ContentType = "text/plain;charset=utf-8";
            //        // 進程名
            //        // var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //        // var configval = _configuration["MyKey"];
            //        await context.Response.WriteAsync("this is a 第一 hello world ");
            //        logger.LogInformation("MW1:傳入請求一");

            //        // 第二次調用
            //        await context.Response.WriteAsync("this is a second hello world ");
            //        logger.LogInformation("MW1:傳入請求二");
            //    });


            //    endpoints.MapGet("/test", async context =>
            //    {
            //        //避免亂碼
            //        context.Response.ContentType = "text/plain;charset=utf-8";

            //        await context.Response.WriteAsync("this is a 第二 test hello world ");
            //    });
            //});

            //app.UseFileServer();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //throw new Exception("您的請求在管道中發生了一些異常，請檢查。");
            //        await context.Response.WriteAsync("Hello World ");
            //        //await context.Response.WriteAsync("Hosting Environment: " + env.EnvironmentName);
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
