using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using WorkflowBlazor.Components;
using WorkflowBlazor.WorkflowJobs.FirstJob;
using WorkflowBlazor.WorkflowJobs.FirstJob.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace WorkflowBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddWorkflow(options =>
            {
                options.UseSqlite("Data Source=workflow.db", true);
            });

            builder.Services.AddWorkflowDSL();

            //ServiceLocator.Instance = builder.Services!.BuildServiceProvider();


            var app = builder.Build();

            ServiceLocator.Instance = app.Services;

            UseWorkflow(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();



            app.Run();
        }

        public static void UseWorkflow(WebApplication app)
        {
            var host = app.Services.GetService<IWorkflowHost>();

            #region 工步注册
            //c#代码注册
            host?.RegisterWorkflow<HelloWorldWorkflow>();
            //json注册
            var json= File.ReadAllText("WorkflowJobs/WorlflowJson/HelloWorldJson.json");
            var loader = app.Services.GetService<IDefinitionLoader>();
            loader?.LoadDefinition(json, Deserializers.Json);



            #endregion

            host?.Start();
            // 通过DI获取IHostApplicationLifetime实例
            var applicationLifetime = app.Services.GetService(typeof(IHostApplicationLifetime)) as IHostApplicationLifetime;
            applicationLifetime?.ApplicationStopping.Register(() =>
            {
                host?.Stop();
            });
        }
    }


}
