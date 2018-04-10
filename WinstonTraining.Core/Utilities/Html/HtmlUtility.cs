using System;
using System.Text.RegularExpressions;

namespace WinstonTraining.Core.Utilities.Html
{
    public static class HtmlUtility
    {
        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
