using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItApi.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base() { }


        public InvalidInputException(string message): base(message){}

        public InvalidInputException(string message, Exception exception ) : base(message, exception) { }
    }
}
