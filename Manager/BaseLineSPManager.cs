using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunarmis.Manager
{
    public static class BaseLineSPManager
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConBaseLine"].ToString();

        public static DataTable GetDistrict(string State)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_DistrictMaster", conn);
                sqlComm.Parameters.AddWithValue("@State", State);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetBlock(string State, string District)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_BlockMaster", conn);
                sqlComm.Parameters.AddWithValue("@State", State);
                sqlComm.Parameters.AddWithValue("@District", District);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }
        }
        public static DataSet GetSPDashboard(string State, string District)
        {
            DataSet dt = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_Dashboard", conn);
                sqlComm.Parameters.AddWithValue("@State", State);
                sqlComm.Parameters.AddWithValue("@District", District);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }

            //StoredProcedure sp = new StoredProcedure("SP_Dashboard");
            //sp.Command.AddParameter("@State", State, DbType.String);
            //sp.Command.AddParameter("@District", District, DbType.String);
            //DataSet ds = sp.ExecuteDataSet();
            //return ds;
        }
        public static DataSet GetSPDashboardNew(string State, string District)
        {            
            DataSet dt = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_Dashboard_New", conn);
                sqlComm.Parameters.AddWithValue("@State", State);
                sqlComm.Parameters.AddWithValue("@District", District);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }

            //StoredProcedure sp = new StoredProcedure("SP_Dashboard_New");
            //sp.Command.AddParameter("@State", State, DbType.String);
            //sp.Command.AddParameter("@District", District, DbType.String);
            //DataSet ds = sp.ExecuteDataSet();
            //return ds;
        }
        public static DataTable GetSubmissionDateSQL()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_GetSubmissionDateSQL", conn);
                //sqlComm.Parameters.AddWithValue("@State", State);
                //sqlComm.Parameters.AddWithValue("@District", District);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }

            //StoredProcedure sp = new StoredProcedure("SP_GetSubmissionDateSQL");
            //DataTable dt = sp.ExecuteDataSet().Tables[0];
            //return dt;
        }
        public static DataTable GetLastLogin(string UserName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_LastLogin", conn);
                sqlComm.Parameters.AddWithValue("@UserName", UserName);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }

            //StoredProcedure sp = new StoredProcedure("SP_LastLogin");
            //sp.Command.AddParameter("@UserName", UserName, DbType.String);
            //DataTable dt = sp.ExecuteDataSet().Tables[0];
            //return dt;
        }
        public static DataTable GetToolDetails(string State, string District, string Block, int ToolName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("SP_ToolDetails", conn);
                sqlComm.Parameters.AddWithValue("@State", State);
                sqlComm.Parameters.AddWithValue("@District", District);
                sqlComm.Parameters.AddWithValue("@Block", Block);
                sqlComm.Parameters.AddWithValue("@ToolName", ToolName);

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 10000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }

            //StoredProcedure sp = new StoredProcedure("SP_ToolDetails");
            //sp.Command.AddParameter("@State", State, DbType.String);
            //sp.Command.AddParameter("@District", District, DbType.String);
            //sp.Command.AddParameter("@Block", Block, DbType.String);
            //sp.Command.AddParameter("@ToolName", ToolName, DbType.Int16);
            //DataTable dt = sp.ExecuteDataSet().Tables[0];
            //return dt;
        }





        //public DataTable sp_GetWNB_Report(string dist, DateTime fromDate, DateTime toDate)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    {
        //        SqlCommand sqlComm = new SqlCommand("sp_WNB_Report", conn);
        //        sqlComm.Parameters.AddWithValue("@dst", dist);
        //        sqlComm.Parameters.AddWithValue("@FromDate", fromDate.ToString());
        //        sqlComm.Parameters.AddWithValue("@ToDate", toDate.ToString());

        //        sqlComm.CommandType = CommandType.StoredProcedure;
        //        sqlComm.CommandTimeout = 10000;
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = sqlComm;

        //        da.Fill(dt);
        //        return dt;
        //    }
        //}
    }
}
