using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    [Serializable]
    public class SingletonException : Exception
    {
        public SingletonException(string text) : base(text) { }
        public SingletonException(string text,Exception e) : base(text,e) { }
    }
}
