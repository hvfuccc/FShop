using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Service.Dto.Products.Client
{
    public class ProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}