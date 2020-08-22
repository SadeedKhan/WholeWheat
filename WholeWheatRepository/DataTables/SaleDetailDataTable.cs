using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeWheatRepository.Models;

namespace WholeWheatRepository.DataTables
{
    public class SaleDetailDataTable
    {
        public DataTable DataTable { get; private set; }

        public SaleDetailDataTable()
        {
            DataTable = new DataTable();

            DataTable.Columns.Add(new DataColumn("sale_detail_id", typeof(long)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("sub_menu_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("quantity", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("price", typeof(long)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("net_total", typeof(long)) { AllowDBNull = true });
        }

        public void FillDataTable(List<ManageSaleDetail> list)
        {
            if (list == null || list.Count == 0)
                return;
            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["sale_detail_id"] = currentObj.SaleDetailId;
                currentRow["sub_menu_id"] = currentObj.SubMenuId;
                currentRow["quantity"] = currentObj.Quantity;
                currentRow["price"] = currentObj.Price;
                currentRow["net_total"] = currentObj.NetTotal;
                DataTable.Rows.Add(currentRow);
            }
        }

        public void Dispose()
        {
            if (DataTable == null)
                return;

            DataTable.Dispose();
            DataTable = null;
        }
    }
}
