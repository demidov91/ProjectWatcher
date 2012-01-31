using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using DAL.Interface;
using ProjectWatcher.Helpers;
using ProjectWatcher.Models.Project.Index;

namespace ProjectWatcher.Models.Helpers
{
    public static class HistoryHelper
    {



        public static MvcHtmlString ShowAll(this HistoryModel model, String culture)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ResourcesHelper.GetText("PropertyName", culture));
            builder.Append(model.SystemName);
            builder.Append('\n');
            foreach (IHistory changing in model.GetEvents())
            {
                builder.Append(ResourcesHelper.GetText("at", culture));
                builder.Append(' ');
                builder.Append(changing.Time);
                builder.Append("  \n");
                builder.Append(changing.FormerValue);
                builder.Append("\n   ");
                builder.Append(ResourcesHelper.GetText("ByWhom", culture));
                builder.Append(changing.Author);
                builder.Append('\n');
            }
            return new MvcHtmlString(builder.ToString());
        }
    }
}