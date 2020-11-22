using CommandLine;
using Newtonsoft.Json;

namespace TemplateConsoleProject.Options
{
    public class BaseOptions
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
