using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using anrControls;

namespace ProjectWatcher.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Restart helper. Deprecated.
        /// </summary>
        /// <returns>Always true.</returns>
        internal static bool Load()
        {
            return true;
        }

        /// <summary>
        /// Converts user-input formatted string into html tags
        /// </summary>
        /// <param name="markdown">Formatted string.</param>
        /// <returns>Html to paste on page.</returns>
        internal static String FromMarkdownToHtml(this String markdown)
        {
            Markdown converter = new Markdown();
            return converter.Transform(markdown);    
        }


        

    }
}