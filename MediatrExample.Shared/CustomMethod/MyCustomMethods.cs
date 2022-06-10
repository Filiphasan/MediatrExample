namespace MediatrExample.Shared.CustomMethod
{
    public static class MyCustomMethods
    {
        /// <summary>
        /// Convert Datetime To String. Result Example: 2022.06.04 20:24
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateTimeToMyString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd HH:mm");
        }

        /// <summary>
        /// Convert Datetime To String. Result Example: 2022.06.04 20:24 If null return string empty
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateTimeToMyString(this DateTime? dateTime)
        {
            string result = string.Empty;
            if (dateTime.HasValue) result = dateTime.Value.ToString("yyyy.MM.dd HH:mm");
            return result;
        }

        /// <summary>
        /// Capitalize String. Example Result: "sOme sTr" -> "Some Str"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Capitalize(this string text)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(text) && text.Length < 2)
            {
                result = text.ToUpper();
            }
            else if (!string.IsNullOrEmpty(text) && text.Length > 1)
            {
                var strArr = text.ToLower().Split(' ');
                foreach (var str in strArr)
                {
                    result = $"{result} {str[0].ToString().ToUpper()}{str[1..]}";
                }
            }
            return result;
        }

        /// <summary>
        /// Exception to String For Logging.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string ExpectExceptionMessage(this Exception exception)
        {
            string result = string.Empty;
            if (exception.InnerException != null)
            {
                result = exception.Message + exception.InnerException.ToString();
            }
            else
            {
                result = exception.Message;
            }
            return result;
        }

        /// <summary>
        /// Pagination EF Core Custom, If u dont use this method, best way is ChunkSize for EF Core 6
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageCount"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static IQueryable<T> TryPagination<T>(this IQueryable<T> query, int pageCount, int pageNumber)
        {
            return query.Skip(pageCount * pageNumber).Take(pageCount);
        }
    }
}
