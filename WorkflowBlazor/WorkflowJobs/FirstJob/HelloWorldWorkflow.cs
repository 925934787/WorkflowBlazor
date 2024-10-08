using WorkflowBlazor.WorkflowJobs.FirstJob.Steps;
using WorkflowCore.Interface;

namespace WorkflowBlazor.WorkflowJobs.FirstJob
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorld";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<GoodbyeWorld>();
        }
    }
}
