using System.IO;

namespace CleanCoders.View
{
    public class ViewTemplate
    {
        public string Template { get; private set; }

        public ViewTemplate(string template)
        {
            Template = template;
        }

        public void Replace(string tagName, string content)
        {
            Template = Template.Replace($"${{{tagName}}}", content);
        }

        public string GetContent()
        {
            return Template;
        }


        public static ViewTemplate Create(string templateResource)
        {
            return new ViewTemplate(File.ReadAllText(templateResource));
        }
    }
}
