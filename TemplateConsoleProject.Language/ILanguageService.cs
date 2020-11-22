namespace TemplateConsoleProject.Language
{
    public interface ILanguageService
    {
        string GetText(string code, params string[] entries);
    }
}
