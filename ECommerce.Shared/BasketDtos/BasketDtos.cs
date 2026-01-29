using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.BasketDtos
{
    public  record BasketDtos(string Id,ICollection<BasketItemDtos> Items);

}
