using CommandLine;

namespace TemplateConsoleProject.Options
{
    [Verb("run", HelpText = "This will run the application!")]
    public class RunOptions : BaseOptions
    {
        [Option]
        public string name { get; set; }
    }        
}
