using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StudentManagement
{
    public class Startup
    {
        private IConfiguration _configuration;

        // �[�J�غc���̿�`�J
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���I����setting���Pı,�b�o��middleware�|�w��request�����e�h����l��
            app.UseRouting();

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("abc.html");

            //// �K�[�q�{���
            //app.UseDefaultFiles(defaultFilesOptions);

            //// �K�[�R�A��󤤶���
            //app.UseStaticFiles();

            // �ϥ�UseFileServer�Ӥ��OUseDefaultFiles�MUseStaticFiles
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("default.html");
            app.UseFileServer(fileServerOptions);


            //�o��~�O�u���h�]�w���a��,�ϥ�delegate�h���]�w
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //�קK�ýX
            //        context.Response.ContentType = "text/plain;charset=utf-8";
            //        // �i�{�W
            //        // var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //        // var configval = _configuration["MyKey"];
            //        await context.Response.WriteAsync("this is a �Ĥ@ hello world ");
            //        logger.LogInformation("MW1:�ǤJ�ШD�@");

            //        // �ĤG���ե�
            //        await context.Response.WriteAsync("this is a second hello world ");
            //        logger.LogInformation("MW1:�ǤJ�ШD�G");
            //    });


            //    endpoints.MapGet("/test", async context =>
            //    {
            //        //�קK�ýX
            //        context.Response.ContentType = "text/plain;charset=utf-8";

            //        await context.Response.WriteAsync("this is a �ĤG test hello world ");
            //    });
            //});
        }
    }
}
