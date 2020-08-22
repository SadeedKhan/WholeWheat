using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeWheatRepository.Models;

namespace WholeWheatRepository.Repository
{
    public class SalesRepository
    {
        private static string _connString { get; set; }
        static SalesRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static List<ManageSale> GetAllSales()
        {
            ManageSale sale = null;
            List<ManageSale> mylist = new List<ManageSale>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_sale_select", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sale = new ManageSale();
                            sale.SaleID = Convert.ToInt32(reader["SaleID"]);
                            sale.SaleTypeID = Convert.ToInt32(reader["SaleTypeID"]);
                            sale.SaleType = Convert.ToString(reader["Sale Type"]);
                            sale.ReciptNo = Convert.ToString(reader["Reciept_No"]);
                            sale.SaleDate = Convert.ToDateTime(reader["SaleDate"]);
                            sale.SubTotal = Convert.ToDecimal(reader["SubTotal"]);
                            sale.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                            sale.TotalItems = Convert.ToString(reader["TotalItems"]);
                            sale.Tax = Convert.ToString(reader["Tax"]);
                            sale.TaxAmount = Convert.ToDecimal(reader["TaxAmount"]);
                            sale.DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]);
                            sale.DeliveryCharges = Convert.ToDecimal(reader["DeliveryCharges"]);
                            sale.PaidAmount = Convert.ToDecimal(reader["PaidAmount"]);
                            sale.Balance = Convert.ToDecimal(reader["Balance"]);
                            sale.TakeAwayCustomerName = Convert.ToString(reader["TakeAwayCustomerName"]);
                            sale.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                            sale.CustomerName = Convert.ToString(reader["CustomerName"]);
                            sale.CustomerEmail = Convert.ToString(reader["CustomerEmail"]);
                            sale.CustomerDescription = Convert.ToString(reader["CustomerDescription"]);
                            sale.CustomerAddress = Convert.ToString(reader["CustomerAddress"]);
                            sale.CustomerPhone = Convert.ToString(reader["CustomerPhone"]);
                            sale.DeliveryBoyName = Convert.ToString(reader["DeliveryBoyName"]);
                            sale.DeliveryBoyID = Convert.ToInt32(reader["DeliveryBoyID"]);
                            sale.OrderStatusID = Convert.ToInt32(reader["OrderStatusID"]);
                            sale.OrderStatus = Convert.ToString(reader["Order Status"]);
                            sale.SaleStatusID = Convert.ToInt32(reader["SaleStatusID"]);
                            sale.SaleStatus = Convert.ToString(reader["Sale Status"]);
                            sale.PaymentModeID = Convert.ToInt32(reader["PaymentModeID"]);
                            sale.PaymentMode = Convert.ToString(reader["Payment Mode"]);
                            mylist.Add(sale);
                        }
                    }
                }
                return mylist;
            }
            catch (Exception e)
            {
                sale = null;
                return mylist;
            }
        }

        public static Common DeleteSale(int SaleID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_sale_delete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@psaleid", SaleID);
                    SqlParameter pkid = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pDesc = new SqlParameter("@pFlag_Desc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pkid);
                    cmd.Parameters.Add(pDesc);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    string id = pkid.Value.ToString();
                    string Desc = pDesc.Value.ToString();
                    obj = new Common();
                    obj.pFlag = id;
                    obj.pFlag_Desc = Desc;
                    return obj;
                }
            }
            catch (Exception e)
            {
                obj = new Common();
                obj.pFlag = "0";
                obj.pFlag_Desc = "An Internal Error Occurred";
                return obj;
            }
        }
    }
}
