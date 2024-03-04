using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunarmis.Manager
{
    public static partial class OMSPManager
    {
        #region SP Helper
        public static DataTable Get_OMS_Dashboard(string State, string District)
        {
            StoredProcedure sp = new StoredProcedure("SP_OMS_Dashboard");
            sp.Command.AddParameter("@State", State, DbType.String);
            sp.Command.AddParameter("@District", District, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        public static DataSet Get_OMS_DashboardDrillDownData(string State, string District)
        {
            StoredProcedure sp = new StoredProcedure("SP_OMS_Dashboard_New");
            sp.Command.AddParameter("@State", State, DbType.String);
            sp.Command.AddParameter("@District", District, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion



    }
}
