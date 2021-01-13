using Microsoft.AspNetCore.Html;
using Westwind.AspNetCore.Markdown;

namespace TechnicalDocuIndexer.Web.Util
{
    public static class FileDisplayHelper
    {
        public static HtmlString ParseToHtml(string extension, string fileContent)
        {
            if (extension.EndsWith(FileExtensions.MARKDOWN))
            {
                return Markdown.ParseHtmlString(fileContent);
            }
            else if (extension.EndsWith(FileExtensions.JSON))
            {
                return new HtmlString($"<pre>{fileContent}</pre>");
            }
            else
            {
                return new HtmlString(fileContent);
            }
        }
    }
}