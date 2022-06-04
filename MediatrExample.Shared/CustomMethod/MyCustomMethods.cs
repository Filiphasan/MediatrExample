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
    }
}
