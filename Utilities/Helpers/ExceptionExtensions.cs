using System;

namespace Utilities
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Retrieves the innermost exception. This is usually the exception with the most useful information
        /// </summary>
        /// <param name="exception">The exception to retrieve the innermost exception from</param>
        /// <returns>The innermost exception</returns>
        public static Exception GetInnermostException(this Exception exception)
        {
            if (null == exception.InnerException)
            {
                return exception;
            }

            return GetInnermostException(exception.InnerException);
        }
    }
}
