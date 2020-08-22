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
    public class DeliveryBoyRepository
    {
        private static string _connString { get; set; }
        static DeliveryBoyRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static Common InsertUpdateCustomer(int DeliveryBoyID, string DeliveryBoyName, string Phone, string Email,string Cnic ,string Description, string Address, int StatusID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_deliveryboy_insertupdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pdeliveryboy_id", DeliveryBoyID);
                    cmd.Parameters.AddWithValue("@pdeliveryboy_name", DeliveryBoyName);
                    cmd.Parameters.AddWithValue("@pdeliveryboy_phone", Phone);
                    cmd.Parameters.AddWithValue("@pdeliveryboy_email", Email);
                    cmd.Parameters.AddWithValue("@pdeliveryboy_cnic", Cnic);
                    cmd.Parameters.AddWithValue("@pdeliveryboy_description", Description);
                    cmd.Parameters.AddWithValue("@pdeliveryboy_address", Address);
                    cmd.Parameters.AddWithValue("@status_id", StatusID);
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
        public static List<ManageDeliveryBoy> GetDeliveryBoy(int StatusID)
        {
            ManageDeliveryBoy cus = null;
            List<ManageDeliveryBoy> mylist = new List<ManageDeliveryBoy>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_deliveryboy_Select", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pstatus_id", StatusID);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cus = new ManageDeliveryBoy();
                            cus.DeliveryBoyID = Convert.ToInt32(reader["DeliveryBoyID"]);
                            cus.DeliveryBoyName = Convert.ToString(reader["DeliveryBoyName"]);
                            if (reader["Phone"] == DBNull.Value)
                                cus.Phone = "";
                            else
                                cus.Phone = Convert.ToString(reader["Phone"]);
                            if (reader["Email"] == DBNull.Value)
                                cus.Email = "";
                            else
                                cus.Email = Convert.ToString(reader["Email"]);
                            if (reader["Cnic"] == DBNull.Value)
                                cus.Cnic = "";
                            else
                                cus.Cnic = Convert.ToString(reader["Cnic"]);
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

    }
}
