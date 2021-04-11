using System;

namespace SmartHome.Common.Exceptions
{
    /// <summary>
    /// Represents Not Defined Exception.
    /// </summary>
    public class NotDefinedException : ArgumentException
    {
        /// <summary>
        /// Creates NotDefinedException from the value when it is not defined.
        /// </summary>
        /// <param name="value"></param>
        public NotDefinedException(string value) : base($"'{value}' is not defined.")
        { }
    }
}
