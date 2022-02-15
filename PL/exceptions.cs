using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Exceptions
    {
        [Serializable]
        public class InvalidTextEnteredExceptions : Exception
        {
            public InvalidTextEnteredExceptions() : base() { }
            public InvalidTextEnteredExceptions(string message) : base(message) { }
            public InvalidTextEnteredExceptions(string message, Exception inner) : base(message, inner) { }
            protected InvalidTextEnteredExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public override string ToString()
            {
                return Message + "Invalid input entered";
            }
        }

    }
}
