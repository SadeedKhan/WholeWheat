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
    public class SubMenuRepository
    {
        private static string _connString { get; set; }
        static SubMenuRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static Common InsertUpdateSubMenu(int SubMenuID, string SubMenuName, int Menuid, string Code, double Price, string Image, string Color,string Description, int StatusId)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_submenu_insertupdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@psubmenu_id", SubMenuID);
                    cmd.Parameters.AddWithValue("@psubmenu_name", SubMenuName);
                    cmd.Parameters.AddWithValue("@pmenu_id", Menuid);
                    cmd.Parameters.AddWithValue("@pcode", Code);
                    cmd.Parameters.AddWithValue("@pprice", Price);
                    cmd.Parameters.AddWithValue("@pimage", Image);
                    cmd.Parameters.AddWithValue("@pcolor", Color);
                    cmd.Parameters.AddWithValue("@pdescription", Description);
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
        public static List<ManageSubMenu> GetSubMenu(int StatusID)
        {
            ManageSubMenu Items = null;
            List<ManageSubMenu> mylist = new List<ManageSubMenu>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_submenu_Select", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pstatus_id", StatusID);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Items = new ManageSubMenu();
                            Items.SubMenuID = Convert.ToInt32(reader["SubMenuID"]);
                            Items.SubMenuName = Convert.ToString(reader["SubMenuName"]);
                            Items.MenuID = Convert.ToInt32(reader["MenuID"]);
                            Items.MenuName = Convert.ToString(reader["MenuName"]);
                            Items.Code = Convert.ToString(reader["SubMenuCode"]);
                            Items.Price = Convert.ToDecimal(reader["SalePrice"]);
                            Items.FileName = Convert.ToString(reader["Image"]);
                            if (Items.FileName != null)
                            {
                                Items.FilePath = Convert.ToString("/Image/MenuImages/" + Items.FileName);
                            }
                            Items.Color = Convert.ToString(reader["Color"]);
                            Items.Description = Convert.ToString(reader["Description"]);
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

        public static List<ManageSubMenu> GetAllSubMenuList(int MenuID)
        {
            int StatusID = 0;
            ManageSubMenu Items = null;
            List<ManageSubMenu> mylist = new List<ManageSubMenu>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_sub_menu_List", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pmenu_id", MenuID);
                    cmd.Parameters.AddWithValue("@pstatus_id", StatusID);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Items = new ManageSubMenu();
                            Items.SubMenuID = Convert.ToInt32(reader["SubMenuID"]);
                            Items.SubMenuName = Convert.ToString(reader["SubMenuName"]);
                            Items.MenuID = Convert.ToInt32(reader["MenuID"]);
                            Items.MenuName = Convert.ToString(reader["MenuName"]);
                            Items.Code = Convert.ToString(reader["SubMenuCode"]);
                            Items.Price = Convert.ToDecimal(reader["SalePrice"]);
                            Items.FileName = Convert.ToString(reader["Image"]);
                            if(Items.FileName!=null)
                            {
                                Items.FilePath = Convert.ToString("/Image/MenuImages/" + Items.FileName);
                            }
                            Items.Color = Convert.ToString(reader["Color"]);
                            Items.Description = Convert.ToString(reader["Description"]);
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
