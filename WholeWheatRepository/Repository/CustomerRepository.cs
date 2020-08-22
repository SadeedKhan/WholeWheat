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
   public class CustomerRepository
    {
        private static string _connString { get; set; }
        static CustomerRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static Common InsertUpdateCustomer(int CustomerID, string CustomerName,string Phone, string Email,  string Description, string Address, int StatusID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_customer_insertupdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pcustomer_id", CustomerID);
                    cmd.Parameters.AddWithValue("@pcustomer_name", CustomerName);
                    cmd.Parameters.AddWithValue("@pcustomer_phone", Phone);
                    cmd.Parameters.AddWithValue("@pcustomer_email", Email);
                    cmd.Parameters.AddWithValue("@pcustomer_description", Description);
                    cmd.Parameters.AddWithValue("@pcustomer_address", Address);
                    cmd.Parameters.AddWithValue("@status_id", StatusID);
                    SqlParameter pkid = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pDesc = new SqlParameter("@pFlag_Desc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pCustomerIdOut = new SqlParameter("@pcustomer_id_out", SqlDbType.BigInt, 100) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pkid);
                    cmd.Parameters.Add(pDesc);
                    cmd.Parameters.Add(pCustomerIdOut);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    string id = pkid.Value.ToString();
                    string Desc = pDesc.Value.ToString();
                    long CustomerIdOut = Convert.ToInt64(pCustomerIdOut.Value);
                    obj = new Common();
                    obj.pFlag = id;
                    obj.pFlag_Desc = Desc;
                    obj.CustomerID = CustomerIdOut;
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
        public static List<ManageCustomer> GetCustomer(int StatusID)
        {
            ManageCustomer cus = null;
            List<ManageCustomer> mylist = new List<ManageCustomer>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_customer_Select", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pstatus_id", StatusID);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cus = new ManageCustomer();
                            cus.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                            cus.CustomerName = Convert.ToString(reader["CustomerName"]);
                            if (reader["Phone"] == DBNull.Value)
                                cus.Phone = "";
                            else
                            cus.Phone = Convert.ToString(reader["Phone"]);
                            if (reader["Email"] == DBNull.Value)
                                cus.Email = "";
                            else
                                cus.Email = Convert.ToString(reader["Email"]);
                            if (reader["Address"] == DBNull.Value)
                                cus.Address = "";
                            else
                            cus.Address = Convert.ToString(reader["Address"]);
                            if (reader["Date"] == DBNull.Value)
                                cus.Date = Convert.ToDateTime(DateTime.Now);
                            else
                            cus.Date = Convert.ToDateTime(reader["Date"]);
                            if (reader["Description"] == DBNull.Value)
                                cus.Description = "";
                            else
                            cus.Description = Convert.ToString(reader["Description"]);
                            cus.Status = Convert.ToString(reader["Status"]);
                            cus.StatusID = Convert.ToInt32(reader["StatusID"]);
                            mylist.Add(cus);
                        }
                    }
                }
                return mylist;
            }
            catch (Exception e)
            {
                cus = null;
                return mylist;
            }
        }
        public static ManageCustomer CheckUserExist(string Phone)
        {
            ManageCustomer cus = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_customer_Select_exist", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pphone", Phone);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cus = new ManageCustomer();
                            cus.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                            cus.CustomerName = Convert.ToString(reader["CustomerName"]);
                            if (reader["Phone"] == DBNull.Value)
                                cus.Phone = "";
                            else
                                cus.Phone = Convert.ToString(reader["Phone"]);
                            if (reader["Email"] == DBNull.Value)
                                cus.Email = "";
                            else
                                cus.Email = Convert.ToString(reader["Email"]);
                            if (reader["Address"] == DBNull.Value)
                                cus.Address = "";
                            else
                                cus.Address = Convert.ToString(reader["Address"]);
                            if (reader["Date"] == DBNull.Value)
                                cus.Date = Convert.ToDateTime(DateTime.Now);
                            else
                                cus.Date = Convert.ToDateTime(reader["Date"]);
                            if (reader["Description"] == DBNull.Value)
                                cus.Description = "";
                            else
                                cus.Description = Convert.ToString(reader["Description"]);
                            cus.Status = Convert.ToString(reader["Status"]);
                            cus.StatusID = Convert.ToInt32(reader["StatusID"]);
                        }
                    }
                }
                return cus;
            }
            catch (Exception e)
            {
                cus = null;
                return cus;
            }
        }
    }
}


