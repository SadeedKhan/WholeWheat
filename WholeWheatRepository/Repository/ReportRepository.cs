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
    public class ReportRepository
    {
        private static string _connString { get; set; }
        static ReportRepository()
        {
            _connString = Connection.LiveConnectionString();
        }

        public static List<ManageSale> GetSaleTypeByDate(int SaleTypeID, DateTime FromDate, DateTime EndDate)
        {

            ManageSale Items = null;
            List<ManageSale> mylist = new List<ManageSale>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetSaleByDateRange", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SaleTypeId", SaleTypeID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", EndDate);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Items = new ManageSale();
                            Items.ReciptNo = Convert.ToString(reader["Reciept_No"]);
                            Items.TakeAwayCustomerName = Convert.ToString(reader["TakeAwayCustomerName"]);
                            Items.CustomerName = Convert.ToString(reader["CustomerName"]);
                            Items.DiscountAmount = Convert.ToInt32(reader["DiscountAmount"]);
                            Items.TotalAmount = Convert.ToInt32(reader["TotalAmount"]);
                            Items.TotalItems = Convert.ToString(reader["TotalItems"]);
                            mylist.Add(Items);
                        }
                    }
                }
                return mylist;
            }
            catch (Exception e)
            {
                Items = null;
                return mylist;
            }
        }

        public static List<ManageSaleDetail> GetProductByDate(int ProductID, DateTime FromDate, DateTime EndDate)
        {

            ManageSaleDetail Items = null;
            List<ManageSaleDetail> mylist = new List<ManageSaleDetail>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetProductByDateRange", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubMenuId", ProductID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", EndDate);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Items = new ManageSaleDetail();
                            Items.SubMenuId = Convert.ToInt32(reader["SubMenuID"]);
                            Items.SaleItem = Convert.ToString(reader["SubMenuName"]);
                            Items.Quantity = Convert.ToString(reader["Quantity"]);
                            Items.Price = Convert.ToInt32(reader["Price"]);
                            Items.NetTotal = Convert.ToInt32(reader["Net_Total"]);
                            mylist.Add(Items);
                        }
                    }
                }
                return mylist;
            }
            catch (Exception e)
            {
                Items = null;
                return mylist;
            }
        }
        public static Reports GetReportDetail()
        {
            Reports Re = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_reportdetail", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = null;
                    conn.Open();
                    da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Re = new Reports();
                    if (ds.Tables[0].Rows[0][0] == DBNull.Value)
                        Re.TotalCustomer = "0";
                    else
                        Re.TotalCustomer = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    if (ds.Tables[1].Rows[0][0] == DBNull.Value)
                        Re.TotalSubMenu = "0";
                    else
                        Re.TotalSubMenu = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    if (ds.Tables[2].Rows[0][0] == DBNull.Value)
                        Re.TotalMenu = "0";
                    else
                        Re.TotalMenu = Convert.ToString(ds.Tables[2].Rows[0][0]);
                    if (ds.Tables[3].Rows[0][0] == DBNull.Value)
                        Re.DailySale = "0";
                    else
                        Re.DailySale = Convert.ToString(ds.Tables[3].Rows[0][0]);
                    return Re;
                }
            }
            catch (Exception e)
            {
                Re = null;
                return Re;
            }
        }

        public static List<PieChart> GetPieChartDetail()
        {
            PieChart pc = null;
            List<PieChart> mylist = new List<PieChart>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_currentmonthtopsale", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pc = new PieChart();
                            if (reader["SubMenuName"] == DBNull.Value)
                                pc.SubMenuName = "";
                            else
                                pc.SubMenuName = Convert.ToString(reader["SubMenuName"]);
                            if (reader["Total"] == DBNull.Value)
                                pc.Total = 0;
                            else
                                pc.Total = Convert.ToInt32(reader["Total"]);
                            mylist.Add(pc);
                        }
                    }
                }
                return mylist;
            }
            catch (Exception e)
            {
                pc = null;
                return mylist;
            }
        }

        public static List<MonthlyGraph> MonthlyGraphDetail()
        {
            List<MonthlyGraph> mylist = new List<MonthlyGraph>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_expenserevenuegraph", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = null;
                    conn.Open();
                    da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    MonthlyGraph[] mg = new MonthlyGraph[12];
                    for (int i = 0; i <= 11; i++)
                    {
                        mg[i] = new MonthlyGraph { MonthlyRevenue = Convert.ToInt32(ds.Tables[0].Rows[i][0]), MonthlyExpenses = Convert.ToInt32(ds.Tables[1].Rows[i][0]) };
                        mylist.Add(mg[i]);
                    }
                    return mylist;
                }
            }
            catch (Exception e)
            {
                return mylist;
            }
        }

    }
}
