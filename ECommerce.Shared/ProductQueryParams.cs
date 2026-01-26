using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string ?Search { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = (value <=0) ? 1 : value; }
        }
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private  int _pagesize = DefaultPageSize;

        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = (value <= 0) ? DefaultPageSize : (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
