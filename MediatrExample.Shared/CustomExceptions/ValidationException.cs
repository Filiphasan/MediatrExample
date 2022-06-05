using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.Shared.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("One or more validation errors occurred")
            => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
