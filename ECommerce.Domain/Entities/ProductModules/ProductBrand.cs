using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.ProductModules
{
    public class ProductBrand:BaseEnitiy<int>
    {
        public string Name { get; set; } = default!;
    }
}
