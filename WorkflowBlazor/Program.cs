using MudBlazor.Services;
using WorkflowBlazor.Components;
using WorkflowBlazor.WorkflowJobs.FirstJob;
using WorkflowBlazor.WorkflowJobs.FirstJob.Steps;
using WorkflowCore.Interface;

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
                options.UseSqlite("Data Source=workflow.db",true);
            });

            builder.Services.AddTransient<HelloWorld>();
            builder.Services.AddTransient<GoodbyeWorld>();

            ServiceLocator.Instance = builder.Services!.BuildServiceProvider();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            var host = app.Services.GetService<IWorkflowHost>();
            host.RegisterWorkflow<HelloWorldWorkflow>();
            host.Start();

            host.StartWorkflow("HelloWorld",1);

            app.Run();
        }
    }
}
