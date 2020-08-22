using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WholeWheatRepository.Models
{
    public class ManageSubMenu
    {
        public int SubMenuID { get; set; }
        public string SubMenuName { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public HttpPostedFileWrapper File { get; set; }
        public string ImagePath { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int StatusID { get; set; }
    }
}
