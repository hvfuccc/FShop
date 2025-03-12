using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Utilities.Exceptions
{
    public class FShopException : Exception
    {
        public FShopException()
        {
        }
        public FShopException(string message)
            : base(message)
        {
        }
        public FShopException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}