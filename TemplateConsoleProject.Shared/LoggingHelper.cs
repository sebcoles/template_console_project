using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace TemplateConsoleProject.Shared
{
    public static class LoggingHelper
    {
        /// <summary>
        /// Uses th diagnostic classes to workout the calling method
        /// </summary>
        /// <returns>
        /// String title of the method this is called in
        /// </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMyMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }
    }
}
