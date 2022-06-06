using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Utilities.Helpers
{
    public static class HtmlBreakLineHelper
    {
        public static string StringToHtml(string html) {
            html = Regex.Replace(html, @"\r\n?|\n", "<br/>");
            return html;
        }

    }
}
