using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeWheatRepository.Models
{
    public class ManageDeliveryBoy
    {
        public int DeliveryBoyID { get; set; }

        public string DeliveryBoyName { get; set; }

        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public string Email { get; set; }

        public string Cnic { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public int StatusID { get; set; }
    }
}
