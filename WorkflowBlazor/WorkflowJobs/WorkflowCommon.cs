using System.Reflection;
using WorkflowBlazor.WorkflowJobs.FirstJob;
using WorkflowCore.Interface;
using WorkflowCore.Services;

namespace WorkflowBlazor.WorkflowJobs
{
    public static class WorkflowCommon
    {
        public static void RegisterWorkflows(IServiceCollection services)
        {
            //var host = ServiceLocator.Instance.GetService<IWorkflowHost>();
            //List<WorkflowJobInfo> jobInfos = new List<WorkflowJobInfo>();
            //var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && typeof(IWorkflow).IsAssignableFrom(t));
            //foreach (Type type in types)
            //{
            //    host.RegisterWorkflow<IWorkflow>(type);
            //}
        }
        //扫描实现IWorkflow的类
        public static List<WorkflowJobInfo> GetWorkflowJobInfos()
        {
            List<WorkflowJobInfo> jobInfos = new List<WorkflowJobInfo>();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && typeof(IWorkflow).IsAssignableFrom(t));
            foreach (Type type in types)
            {
                IWorkflow workflowInstance = (IWorkflow)Activator.CreateInstance(type);
                jobInfos.Add(new WorkflowJobInfo() { Id = workflowInstance.Id, Version = workflowInstance.Version });
            }
            return jobInfos;
        }

        public static void StartJob(WorkflowJobInfo jobInfo)
        {
            Console.WriteLine("Start Job: " + jobInfo.Id);
            //var host = ServiceLocator.Instance.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<HelloWorldWorkflow>();

            //host.StartWorkflow(jobInfo.Id, jobInfo.Version,null);

            var service= ServiceLocator.Instance.GetService<IWorkflowController>();
            service?.StartWorkflow(jobInfo.Id, jobInfo.Version, null);

            service?.StartWorkflow("HelloWorldJson", 1, null);


        }
    }

    public class WorkflowJobInfo
    {
        public string Id { get; set; }
        public int Version { get; set; }
    }
}
