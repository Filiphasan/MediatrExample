using FluentValidation;

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
            return dateTime.ToString("dd.MM.yyyy HH:mm");
        }

        /// <summary>
        /// Convert Datetime To String. Result Example: 2022.06.04 20:24 If null return string empty
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateTimeToMyString(this DateTime? dateTime)
        {
            string result = string.Empty;
            if (dateTime.HasValue) result = dateTime.Value.ToString("dd.MM.yyyy HH:mm");
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
            text = text.ToLower().Trim();
            if (!string.IsNullOrEmpty(text) && text.Length < 2)
            {
                result = text.ToUpper();
            }
            else if (!string.IsNullOrEmpty(text) && text.Length > 1)
            {
                result = String.Join(" ", text.Split(' ').Select(x => x.First().ToString().ToUpper() + x[1..]));
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
            string result = exception.Message;
            if (exception.InnerException != null)
            {
                result += exception.InnerException.ToString();
            }
            return result;
        }

        /// <summary>
        /// Pagination EF Core Custom, If u dont use this method, other way is ChunkSize for EF Core 6
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

        /// <summary>
        /// Format GSM, GSM one of (+905XXXXXXXXX, 905XXXXXXXXX, 05XXXXXXXXX, 5XXXXXXXXX) return 5XXXXXXXXX other return string.Empty (I know this method is not best practices but I dont have enough level regex)
        /// </summary>
        /// <param name="gsm"></param>
        /// <returns></returns>
        public static string FormatGSMForTR(this string gsm)
        {
            string result = string.Empty;
            string formatGsm = string.Empty;

            if (gsm is null)
            {
                return result;
            }

            gsm = gsm.Replace("-", string.Empty).Replace(" ", string.Empty);

            if (gsm.Length < 10 || gsm.Length > 13)
            {
                return result;
            }
            else
            {
                if (gsm.StartsWith("+90")) formatGsm = gsm.Substring(3);
                if (gsm.StartsWith("90")) formatGsm = gsm.Substring(2);
                if (gsm.StartsWith("0")) formatGsm = gsm.Substring(1);
                if (formatGsm.StartsWith("5") && formatGsm.Length == 10 && long.TryParse(gsm, out _))
                {
                    result = formatGsm;
                }
            }
            return result ?? string.Empty;
        }

        /// <summary>
        /// Custom Basic Object Mapper
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="obje"></param>
        /// <returns></returns>
        public static TDestination ObjectMapper<TDestination, TSource>(this TSource obje) where TSource : class, new() where TDestination : class, new()
        {
            var sourceObjProperties = obje.GetType().GetProperties();
            var destination = new TDestination();
            var destinationObjProperties = destination.GetType().GetProperties();
            foreach (var sourceItem in sourceObjProperties)
            {
                if (destinationObjProperties.Any(x => x.Name == sourceItem.Name))
                {
                    var destinationItem = destinationObjProperties.FirstOrDefault(x => x.Name == sourceItem.Name);
                    destinationItem.SetValue(destination, sourceItem.GetValue(obje));
                }
            }
            return destination;
        }

        /// <summary>
        /// Custom Password Validator For FluentValidation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="property"></param>
        /// <param name="pwRegEx"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> CheckMyPassword<T>(this IRuleBuilder<T, string> ruleBuilder, string pwRegEx = null, string errorMessage = null)
        {
            if (pwRegEx is null) pwRegEx = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,16}$";
            if (errorMessage is null) errorMessage = "Password must contain one uppercase, one lowercase, one number. Password length must beetween 6 and 16.";
            if (pwRegEx is null) pwRegEx = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,16}$";
            return ruleBuilder.NotNull().Matches(pwRegEx)
                .WithMessage(errorMessage);
        }
    }
}
