using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Helpers
{
    public static class StringHelper
    {
        private static anrControls.Markdown converter;

        /// <summary>
        /// Restart helper.
        /// </summary>
        /// <returns></returns>
        internal static bool Load()
        {
            try
            {
                converter = new anrControls.Markdown();
                return converter != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Converts user-input formatted string into html tags
        /// </summary>
        /// <param name="markdown">Formatted string.</param>
        /// <returns>Html to paste on page.</returns>
        internal static String FromMarkdownToHtml(this String markdown)
        {
            return converter.Transform(markdown);    
        }


        

    }
}