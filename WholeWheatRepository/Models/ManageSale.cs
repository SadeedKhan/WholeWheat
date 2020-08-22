using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeWheatRepository.Models
{
    public class ManageSale
    {
        public int SaleID { get; set; }

        public int SaleTypeID { get; set; }

        public string SaleType { get; set; }

        public string ReciptNo { get; set; }

        public string TotalItems { get; set; }

        public DateTime SaleDate { get; set; }

        public string Tax { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal DeliveryCharges { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal Balance { get; set; }

        public string TakeAwayCustomerName { get; set; }

        public string TableNo { get; set; }

        public int DeliveryBoyID { get; set; }

        public string DeliveryBoyName { get; set; }

        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerDescription { get; set; }

        public string CustomerAddress { get; set; }

        public int PaymentModeID { get; set; }

        public string PaymentMode { get; set; }

        public int SaleStatusID { get; set; }

        public string SaleStatus { get; set; }

        public int OrderStatusID { get; set; }

        public string OrderStatus { get; set; }

        public int StatusID { get; set; }

        public string Status { get; set; }

    }
}
