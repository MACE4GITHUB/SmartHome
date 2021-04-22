namespace SmartHome.Common.Extentions
{
    /// <summary>
    /// Represents extentions for correcting string.
    /// </summary>
    public static class StringExtentions
    {
        /// <summary>
        /// Gets true when value is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) =>
            string.IsNullOrEmpty(value);

        /// <summary>
        /// Gets true when value is Null or Empty or White Space otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value) =>
            string.IsNullOrWhiteSpace(value);
    }
}
