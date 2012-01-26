using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace ProjectWatcher.Helpers
{
    public static class HtmlHelperHelper
    {
        public static MvcHtmlString GetWidthTd(this HtmlHelper helper, String content, int width)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<td style=""width: ");
            builder.Append(width);
            builder.Append("%;\">\n" + content);
            builder.Append('\n');
            builder.Append(@"</td>");
            builder.Append('\n');
            return new MvcHtmlString(builder.ToString());
        }
    }
}