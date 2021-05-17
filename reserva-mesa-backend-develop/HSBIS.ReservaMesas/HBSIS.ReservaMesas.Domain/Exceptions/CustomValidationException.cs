using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HBSIS.ReservaMesas.Domain.Exceptions
{
    public class CustomValidationException : AggregateException
    {
        public CustomValidationException() : base() { }
        public CustomValidationException(string message) : base(message) { }
        public CustomValidationException(IEnumerable<Exception> innerExceptions) : base(innerExceptions) { }
        public CustomValidationException(string message, Exception inner) : base(message, inner) { }
        protected CustomValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
