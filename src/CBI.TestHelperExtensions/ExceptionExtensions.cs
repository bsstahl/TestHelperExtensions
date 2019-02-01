using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TestHelperExtensions
{

    /// <summary>
    /// Adds functionality that is often used for unit testing
    /// to the Exception data type and its derived classes
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Throws the specified exception if the predicate results in a true value
        /// based on the parameter supplied
        /// </summary>
        /// <typeparam name="T">The data type of the parameter</typeparam>
        /// <param name="exception">The exception to be thrown (conditionally)</param>
        /// <param name="predicate">A function returning a bool containing functionality
        /// to determine if the exception should be thrown or not</param>
        /// <param name="parameter">The parameter value used in the predicate function</param>
        public static void ThrowIf<T>(this Exception exception, Func<T, bool> predicate, T parameter)
        {
            if (predicate(parameter))
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));
                else
                    throw exception;
        }
    }
}
