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
    public class ExpenseRepository
    {
        private static string _connString { get; set; }
        static ExpenseRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        public static Common InsertUpdateExpense(int ExpenseID, int ExpenseNameID, DateTime ExpenseDate, string ReferenceNo, int ExpenseHeadID,decimal ExpenseAmount, string ExpenseImage, string Notes,int StatusID)
        {
            Common obj = null;
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_expense_insertupdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pexpense_id", ExpenseID);
                    cmd.Parameters.AddWithValue("@pexpensenameid", ExpenseNameID);
                    cmd.Parameters.AddWithValue("@pexpensedate", ExpenseDate);
                    cmd.Parameters.AddWithValue("@preferenceno", ReferenceNo);
                    cmd.Parameters.AddWithValue("@pexpensehead_id", ExpenseHeadID);
                    cmd.Parameters.AddWithValue("@pexpenseamount", ExpenseAmount);
                    cmd.Parameters.AddWithValue("@pexpenseimage", ExpenseImage);
                    cmd.Parameters.AddWithValue("@pnotes", Notes);
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
        public static List<ManageExpense> GetExpense(int StatusID)
        {
            ManageExpense Items = null;
            List<ManageExpense> mylist = new List<ManageExpense>();
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("proc_expense_Select", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pstatus_id", StatusID);
                    SqlDataReader reader = null;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Items = new ManageExpense();
                            Items.ExpenseID = Convert.ToInt32(reader["ExpenseID"]);
                            Items.ExpenseNameID = Convert.ToInt32(reader["ExpenseNameID"]);
                            Items.ExpenseName = Convert.ToString(reader["ExpenseName"]);
                            Items.ExpenseDate = Convert.ToDateTime(reader["ExpenseDate"]);
                            Items.ReferenceNo = Convert.ToString(reader["ReferenceNo"]);
                            Items.ExpenseHeadID = Convert.ToInt32(reader["ExpenseHeadID"]);
                            Items.ExpenseHeadName = Convert.ToString(reader["ExpenseHeadName"]);
                            Items.ExpenseAmount = Convert.ToDecimal(reader["ExpenseAmount"]);
                            Items.FileName = Convert.ToString(reader["ExpenseImage"]);
                            if (Items.FileName != null)
                            {
                                Items.FilePath = Convert.ToString("/Image/ExpenseImages/" + Items.FileName);
                            }
                            Items.Notes = Convert.ToString(reader["Notes"]);
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
