using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeWheatRepository.Models
{
    public class ManageSaleDetail
    {
        public long SaleDetailId { get; set; }
        public long SaleId { get; set; }
        public int SubMenuId { get; set; }
        public string SaleItem { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal NetTotal { get; set; }
    }
}
