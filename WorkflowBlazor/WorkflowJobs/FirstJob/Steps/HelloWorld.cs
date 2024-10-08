using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowBlazor.WorkflowJobs.FirstJob.Steps
{
    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hello world");
            Console.ResetColor();
            return ExecutionResult.Next();
        }
    }
}
