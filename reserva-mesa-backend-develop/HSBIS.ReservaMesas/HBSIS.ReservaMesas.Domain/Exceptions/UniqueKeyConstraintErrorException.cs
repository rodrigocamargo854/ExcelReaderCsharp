using System;
using System.Runtime.Serialization;

namespace HBSIS.ReservaMesas.Domain.Exceptions
{
    public class UniqueKeyConstraintErrorException : AggregateException
    {
        public UniqueKeyConstraintErrorException(string message) : base(message) { }
        protected UniqueKeyConstraintErrorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
