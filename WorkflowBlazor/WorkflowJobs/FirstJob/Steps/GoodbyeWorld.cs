using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowBlazor.WorkflowJobs.FirstJob.Steps
{
    public class GoodbyeWorld: StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Goodbye world");
            Console.ResetColor();

            return ExecutionResult.Next();
        }
    }
}
