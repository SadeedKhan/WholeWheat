using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeWheatRepository.Repository;

namespace WholeWheatRepository.Models
{
    public class DropDowns
    {
        public List<ManageMenu> GetMenu()
        {
            return MenuRepository.GetMenu(0);
        }
        public List<ManageCustomer> GetCustomer()
        {
            return CustomerRepository.GetCustomer(0);
        }
        public List<ManageDeliveryBoy> GetDeliveryBoy()
        {
            return DeliveryBoyRepository.GetDeliveryBoy(0);
        }
        public List<ManageExpenseHead> GetExpenseHead()
        {
            return ExpenseHeadRepository.GetExpenseHead(0);
        }
        public List<ManageSubMenu> GetSubMenu()
        {
            return SubMenuRepository.GetAllSubMenuList(0);
        }
        public List<ManageExpenseName> GetExpenseName()
        {
            return ExpenseNameRepository.GetExpenseNameDropDown(0);
        }
    }
}
