using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeWheatRepository.DataTables;
using WholeWheatRepository.Models;

namespace WholeWheatRepository.Repository
{
    public class POSRepository
    {
        private static string _connString { get; set; }
        static POSRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static List<Common> SaleInsertUpdate(int SaleID, string TotalItems, decimal SubTotal, decimal Total, decimal DiscountAmount, decimal DeliveryCharges, decimal PaidAmount, decimal Change, int CustomerID, int DeliveryBoyID, string TableNo, string TakeAwayCustomerName, int SaleTypeID, string Tax, decimal TaxAmount, int OrderStatusID, int SaleStatusID, int PaymentModeID, List<ManageSaleDetail> SaleDetailList)
        {
            long CurrentTransactionId = 0;
            Common obje = null;
            List<Common> mylist = new List<Common>();
            SaleDetailDataTable SaleDataTable = new DataTables.SaleDetailDataTable();
            SaleDataTable.FillDataTable(SaleDetailList);
            var dt = SaleDataTable.DataTable;
            bool flag = true;
            int StatusId = 0;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_sale_insertupdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@psale_id", SaleID);
                    cmd.Parameters.AddWithValue("@ptotalitems", TotalItems);
                    cmd.Parameters.AddWithValue("@psubtotal", SubTotal);
                    cmd.Parameters.AddWithValue("@ptotal", Total);
                    cmd.Parameters.AddWithValue("@pdiscount_amount", DiscountAmount);
                    cmd.Parameters.AddWithValue("@pdelivery_charges", DeliveryCharges);
                    cmd.Parameters.AddWithValue("@ppaid_amount", PaidAmount);
                    cmd.Parameters.AddWithValue("@pbalance", Change);
                    cmd.Parameters.AddWithValue("@pcustomer_id", CustomerID);
                    cmd.Parameters.AddWithValue("@pdelivery_boy_id", DeliveryBoyID);
                    cmd.Parameters.AddWithValue("@ptable_no", TableNo);
                    cmd.Parameters.AddWithValue("@ptakeawaycustomername", TakeAwayCustomerName);
                    cmd.Parameters.AddWithValue("@psaletype_id", SaleTypeID);
                    cmd.Parameters.AddWithValue("@ptax", Tax);
                    cmd.Parameters.AddWithValue("@ptaxamount", TaxAmount);
                    cmd.Parameters.AddWithValue("@porder_status_id", OrderStatusID);
                    cmd.Parameters.AddWithValue("@psale_status_id", SaleStatusID);
                    cmd.Parameters.AddWithValue("@pPayment_mode_id", PaymentModeID);
                    cmd.Parameters.AddWithValue("@status_id", StatusId);
                    SqlParameter pkid = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pDesc = new SqlParameter("@pFlag_Desc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pReceiptNoOut = new SqlParameter("@preceipt_no_out", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
                    SqlParameter pSaleIdOut = new SqlParameter("@psale_id_out", SqlDbType.BigInt, 100) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pkid);
                    cmd.Parameters.Add(pDesc);
                    cmd.Parameters.Add(pReceiptNoOut);
                    cmd.Parameters.Add(pSaleIdOut);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    string id = pkid.Value.ToString();
                    string Desc = pDesc.Value.ToString();
                    string RecieptNo = pReceiptNoOut.Value.ToString();
                    long SaleIdOut = Convert.ToInt64(pSaleIdOut.Value);
                    obje = new Common();
                    obje.pFlag = id;
                    obje.pFlag_Desc = Desc;
                    obje.TransactionId = SaleIdOut;
                    obje.TransactionNo = RecieptNo;
                    mylist.Add(obje);
                    CurrentTransactionId = SaleIdOut;
                    if (id == "1")
                    {
                        using (var connection = new SqlConnection(_connString))
                        {
                            SqlCommand command = new SqlCommand("proc_sale_detail_insertupdate", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@dt", dt);
                            command.Parameters.AddWithValue("@psale_id", CurrentTransactionId);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                //return flag;
                return mylist;
            }
            catch (Exception e)
            {
                obje = new Common();
                obje.pFlag = "0";
                obje.pFlag_Desc = "An Internal Error Occurred";
                mylist.Add(obje);
                return mylist;
            }
        }

        public static Common SalePaymentUpdate(int SaleID, decimal PaidAmount, decimal Change, string TableNo, int SaleStatusID, int PaymentModeID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_salepayment_update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@psale_id", SaleID);
                    cmd.Parameters.AddWithValue("@ppaid_amount", PaidAmount);
                    cmd.Parameters.AddWithValue("@pbalance", Change);
                    cmd.Parameters.AddWithValue("@ptable_no", TableNo);
                    cmd.Parameters.AddWithValue("@psale_status_id", SaleStatusID);
                    cmd.Parameters.AddWithValue("@pPayment_mode_id", PaymentModeID);
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

        public static List<ManageSale> GetPendingSales()
        {
            ManageSale sale = null;
            List<ManageSale> mylist = new List<ManageSale>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_pending_sale_select", conn);
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
        public static List<ManageSale> GetTodayPendingSales()
        {
            ManageSale sale = null;
            List<ManageSale> mylist = new List<ManageSale>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_today_pending_sale_select", conn);
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

        public static List<ManageSale> GetDineInPendingSales()
        {
            ManageSale sale = null;
            List<ManageSale> mylist = new List<ManageSale>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_pending_dinein_sale_select", conn);
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
                            sale.PaidAmount = Convert.ToDecimal(reader["PaidAmount"]);
                            sale.Balance = Convert.ToDecimal(reader["Balance"]);
                            sale.TableNo = Convert.ToString(reader["TableNo"]);
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

        public static List<ManageSaleDetail> GetSalesDetails(int SaleID)
        {
            ManageSaleDetail saledetail = null;
            List<ManageSaleDetail> mylist = new List<ManageSaleDetail>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_sale_detail_Select", conn);
                    cmd.Parameters.AddWithValue("@psale_id", SaleID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            saledetail = new ManageSaleDetail();
                            saledetail.SaleDetailId = Convert.ToInt32(reader["SaleDetailID"]);
                            saledetail.SaleId = Convert.ToInt32(reader["SaleID"]);
                            saledetail.SubMenuId = Convert.ToInt32(reader["SubMenuID"]);
                            saledetail.SaleItem = Convert.ToString(reader["Sub Menu"]);
                            saledetail.Quantity = Convert.ToString(reader["quantity"]);
                            saledetail.Price = Convert.ToInt32(reader["price"]);
                            saledetail.NetTotal = Convert.ToInt32(reader["net_total"]);
                            mylist.Add(saledetail);
                        }
                    }
                }
                return mylist;
            }
            catch (Exception e)
            {
                saledetail = null;
                return mylist;
            }
        }

        public static Common OrderDeliveredStatus(int SaleID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_order_status_update", conn);
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


        public static Common DeleteSaleDetailRow(int SaleDetailID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_saledetailrow_delete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@psaledetailid", SaleDetailID);
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
