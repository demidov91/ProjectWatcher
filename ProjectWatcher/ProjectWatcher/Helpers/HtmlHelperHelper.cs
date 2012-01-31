using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using DAL.Interface;
using ProjectWatcher.Models.Project.Index;

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

        public static String RenderValue(Object value, String type)
        {
            switch (type)
            {
                case "String":
                    return ((String)value).FromMarkdownToHtml();
                case "Percentage":
                    return value.ToString() + "%";
                 
                default:
                    return value.ToString();
            }
 
        }

        /// <summary>
        /// Converts Value of typed ValueModel into html
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value"></param>
        /// <param name="type">One of the basic progrm types</param>
        /// <returns>Html for illustration a value, not for editing.</returns>
        public static MvcHtmlString RenderValue(this HtmlHelper helper, Object value, String type)
        {
            String result = RenderValue(value, type);
            return new MvcHtmlString(result);
        }

        /// <summary>
        /// Converts model into html 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <returns>Html for illustration model on the page, not for editig.</returns>
        public static MvcHtmlString RenderModel(this HtmlHelper helper, ValueModel model)
        {
            return helper.RenderValue(model.Value, model.Type);
        }

    }
}