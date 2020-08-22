using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WholeWheatRepository.Models
{
    public class ManageExpense
    {
        public int ExpenseID { get; set; }

        public int ExpenseNameID { get; set; }

        public string ExpenseName { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string ReferenceNo { get; set; }

        public int ExpenseHeadID { get; set; }

        public string ExpenseHeadName { get; set; }

        public decimal ExpenseAmount { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public HttpPostedFileWrapper File { get; set; }

        public string ExpenseImage { get; set; }

        public string Notes { get; set; }

        public int StatusID { get; set; }

        public string Status { get; set; }
    }
}
