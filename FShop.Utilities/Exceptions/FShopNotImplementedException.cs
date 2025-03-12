using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Utilities.Exceptions
{
    public class FShopNotImplementedException : NotImplementedException
    {
        public FShopNotImplementedException()
            : base("Chức năng này chưa được triển khai")
        {
        }
        public FShopNotImplementedException(string message)
            : base(message)
        {
        }
        public FShopNotImplementedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
