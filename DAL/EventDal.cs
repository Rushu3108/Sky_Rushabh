using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Models;

namespace DAL
{
    public class EventDal
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        public int ExecuteNoneQuery(string SPName, SqlParameter[] p)
        {
            int RowEffected = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(p);
                con.Open();
                RowEffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                con.Close();
            }
            return RowEffected;
        }
        public DataTable GetDataTable(string SPName, SqlParameter[] p)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(p);
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public int DeleteEvent(int Id)
        {
            int iRet = 0;
            try
            {
                List<SqlParameter> li = new List<SqlParameter>();
                li.Add(new SqlParameter("@Flag", "DELETE"));
                li.Add(new SqlParameter("@Id", Id));
                iRet = ExecuteNoneQuery("sp_Event",li.ToArray());
            }
            catch(Exception ex)
            {
                iRet = 0;
            }
            return iRet;
        }
        public int InsertEventData(EventModel objEventModel)
        {
            int iRet = 0;
            try
            {
                List<SqlParameter> li = new List<SqlParameter>();
                li.Add(new SqlParameter("@Flag", "ADD"));
                li.Add(new SqlParameter("@Id", objEventModel.id));
                li.Add(new SqlParameter("@Event_Name", objEventModel.Event_Name));
                li.Add(new SqlParameter("@StartDate", objEventModel.StartDate));
                li.Add(new SqlParameter("@EndDate", objEventModel.EndDate));
                li.Add(new SqlParameter("@Event_des", objEventModel.Event_des));
                li.Add(new SqlParameter("@Location", objEventModel.Location));
                iRet = ExecuteNoneQuery("sp_Event", li.ToArray());
            }
            catch (Exception ex)
            {
                iRet = 0;
            }
            return iRet;
        }

        public EventModel GetEventById(int Id)
        {
            EventModel objEventModel = new EventModel();
            DataTable objDt = new DataTable();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", "GET"));
            param.Add(new SqlParameter("@Id", Id));
            objDt = GetDataTable("sp_Event", param.ToArray());
            if (objDt != null && objDt.Rows.Count > 0)
            {
                objEventModel.id = Convert.ToInt32(objDt.Rows[0]["id"].ToString());
                objEventModel.Event_Name = objDt.Rows[0]["Event_Name"].ToString();
                objEventModel.StartDate = Convert.ToDateTime(objDt.Rows[0]["StartDate"].ToString());
                objEventModel.EndDate = Convert.ToDateTime(objDt.Rows[0]["EndDate"].ToString());
                objEventModel.Event_des = Convert.ToString(objDt.Rows[0]["Event_des"].ToString());
                objEventModel.Location = Convert.ToString(objDt.Rows[0]["Location"].ToString());               
             }
            else
                objEventModel = new EventModel();

            return objEventModel;
        }

        public List<EventModel> GetEventData()
        {
            List<EventModel> li = new List<EventModel>();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", "GET"));            
            DataTable objdt = GetDataTable("sp_Event", param.ToArray());
            if (objdt != null && objdt.Rows.Count > 0)
            {
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    EventModel objEventModel = new EventModel();
                    objEventModel.id = Convert.ToInt32(objdt.Rows[i]["id"].ToString());
                    objEventModel.Event_Name = Convert.ToString(objdt.Rows[i]["Event_Name"].ToString());
                    objEventModel.StartDate = Convert.ToDateTime(objdt.Rows[i]["StartDate"].ToString());
                    objEventModel.EndDate = Convert.ToDateTime(objdt.Rows[i]["EndDate"].ToString());
                    objEventModel.Event_des = Convert.ToString(objdt.Rows[i]["Event_des"].ToString());
                    objEventModel.Location = Convert.ToString(objdt.Rows[i]["Location"].ToString());
                    li.Add(objEventModel);
                }
            }
            return li;
        }

        public List<EventModel> EventDataListPassed()
        {
            List<EventModel> li = new List<EventModel>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Flag", "GET"));
                DataSet objds = new DataSet();
                SqlCommand cmd = new SqlCommand("sp_Event", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param.ToArray());
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(objds);

                if (objds != null && objds.Tables.Count > 1 && objds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < objds.Tables[1].Rows.Count; i++)
                    {
                        EventModel objEventModel = new EventModel();
                        objEventModel.id = Convert.ToInt32(objds.Tables[1].Rows[i]["id"].ToString());
                        objEventModel.Event_Name = Convert.ToString(objds.Tables[1].Rows[i]["Event_Name"].ToString());
                        objEventModel.StartDate = Convert.ToDateTime(objds.Tables[1].Rows[i]["StartDate"].ToString());
                        objEventModel.EndDate = Convert.ToDateTime(objds.Tables[1].Rows[i]["EndDate"].ToString());
                        objEventModel.Event_des = Convert.ToString(objds.Tables[1].Rows[i]["Event_des"].ToString());
                        objEventModel.Location = Convert.ToString(objds.Tables[1].Rows[i]["Location"].ToString());
                        li.Add(objEventModel);
                    }
                }
            }catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return li;
        }

        
    }
}