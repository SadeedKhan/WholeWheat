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
    public class MenuRepository
    {
        private static string _connString { get; set; }
        static MenuRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static Common InsertUpdateMenu(int Menuid, string Menuname, int StatusId)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_menu_insertupdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pmenu_id", Menuid);
                    cmd.Parameters.AddWithValue("@pmenu_name", Menuname);
                    cmd.Parameters.AddWithValue("@status_id", StatusId);
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
        public static List<ManageMenu> GetMenu(int StatusID)
        {
            ManageMenu Items = null;
            List<ManageMenu> mylist = new List<ManageMenu>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_menu_Select", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pstatus_id", StatusID);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Items = new ManageMenu();
                            Items.MenuID = Convert.ToInt32(reader["MenuID"]);
                            Items.MenuName = Convert.ToString(reader["MenuName"]);
                            Items.Status = Convert.ToString(reader["Status"]);
                            Items.StatusID = Convert.ToInt32(reader["StatusID"]);
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
    }
}
