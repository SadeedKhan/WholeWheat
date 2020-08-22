using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeWheatRepository.Models
{
    public class ManageExpenseName
    {
        public int ExpenseNameID { get; set; }
        public string ExpenseName { get; set; }
        public string Status { get; set; }
        public int StatusID { get; set; }
    }
}
