using System.Diagnostics.CodeAnalysis;

namespace WorkflowBlazor
{
    public static class ServiceLocator
    {
        [AllowNull]
        public static IServiceProvider Instance { get; set; }
    }
}
