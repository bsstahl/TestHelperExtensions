using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TestHelperExtensions
{
    public static class ExceptionExtensions
    {
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
