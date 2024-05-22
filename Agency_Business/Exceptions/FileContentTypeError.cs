using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency_Business.Exceptions
{
    public class FileContentTypeError : Exception
    {
        public string PropertyName { get; set; }
        public FileContentTypeError(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
