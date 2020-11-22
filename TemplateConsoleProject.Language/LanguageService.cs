using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TemplateConsoleProject.Language.Models;

namespace TemplateConsoleProject.Language
{
    /// <summary>
    /// The language service reads localization JSON files 
    /// and returns the message that matches the code provided
    /// </summary>
    public class LanguageService : ILanguageService
    {
        private MessageContainer _messages;

        /// <summary>
        /// The JSON files must be contained in the same directory 
        /// as the assembly.
        /// </summary>
        public LanguageService(string localization)
        {
            using StreamReader r =
                new StreamReader(Path.Combine(
                $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}",
                $"{localization}.json"));

            string json = r.ReadToEnd();
            _messages = JsonConvert.DeserializeObject<MessageContainer>(json);

            if (!_messages.messages.Any())
                throw new ArgumentException($"{localization}.json does not contain any log messages!");
        }

        /// <summary>
        /// Finds the matching message for the provided code
        /// </summary>
        /// <returns>
        /// The associated string message from the JSON file
        /// </returns>
        public string GetText(string code, params string[] entries)
        {
            foreach (var message in _messages.messages)
                if (message.Code.Equals(code))
                    return String.Format(message.Value, entries);

            throw new ArgumentException($"{code} is not in the resources dictionary.");
        }
    }
}
