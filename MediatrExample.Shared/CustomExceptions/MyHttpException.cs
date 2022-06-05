using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.Shared.CustomExceptions
{
    public class MyHttpException : Exception
    {
        public MyHttpException(string exceptionMessage, Exception? exception) : base(exceptionMessage, exception)
        {
        }
    }
}
