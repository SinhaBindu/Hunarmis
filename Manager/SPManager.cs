using Hunarmis.Models;
using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hunarmis.Manager
{
    public static partial class SPManager
    {
        public static DataTable SPGetUserlist()
        {
            StoredProcedure sp = new StoredProcedure("SPGetUserlist");
            sp.Command.AddParameter("@User", HttpContext.Current.User.Identity.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_Batch()
        {
            StoredProcedure sp = new StoredProcedure("SP_Batch");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_ParticipantList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ParticipantList");
            sp.Command.AddParameter("@Type", model.Type, DbType.Int32);
            sp.Command.AddParameter("@Search", model.Search, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_PartCallingList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PartCalling");
            sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}
