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
        public static DataTable SP_GetUserList()
        {
            StoredProcedure sp = new StoredProcedure("SP_GetUserList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_Batch()
        {
            StoredProcedure sp = new StoredProcedure("SP_Batch");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_BatchList()
        {
            StoredProcedure sp = new StoredProcedure("SP_BatchList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_CoursesList()
        {
            StoredProcedure sp = new StoredProcedure("SP_CoursesList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_EducationalMList()
        {
            StoredProcedure sp = new StoredProcedure("SP_EducationalMList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        public static DataTable SP_Training_AgencyList()
        {
            StoredProcedure sp = new StoredProcedure("SP_Training_AgencyList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_TrainingCentreList()
        {
            StoredProcedure sp = new StoredProcedure("SP_TrainingCentreList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_ParticipantList(FilterModel model)
        {
            model.CallStatus = model.CallStatus == "-1" ? "" : model.CallStatus;
            StoredProcedure sp = new StoredProcedure("SP_ParticipantList");
            sp.Command.AddParameter("@Type", model.Type, DbType.Int32);
            sp.Command.AddParameter("@Search", model.Search, DbType.String);
            sp.Command.AddParameter("@CallStatus", model.CallStatus, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_PartQuesList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PartQuesList");
            //model.ParticipantId = !string.IsNullOrWhiteSpace(model.ParticipantId) ? model.ParticipantId : "";
            //model.ParticipantQuestionId = !string.IsNullOrWhiteSpace(model.ParticipantQuestionId) ? model.ParticipantQuestionId : "";
            sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.Int32);
            sp.Command.AddParameter("@ParticipantId", model.ParticipantId, DbType.String);
            sp.Command.AddParameter("@ParticipantQuestionId", model.ParticipantQuestionId, DbType.String);
            sp.Command.AddParameter("@UserId", model.UserId, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SP_Dashboard(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_Dashboard");
            //sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            //sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            //sp.Command.AddParameter("@BatchId", model.BatchId, DbType.Int32);
            //sp.Command.AddParameter("@ParticipantQuestionId", model.ParticipantQuestionId, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet Sp_DashboardPartCalling(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("Sp_PartCalling");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_CallChartWiseMonth(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CallChartWiseMonth");
            sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            sp.Command.AddParameter("@UserBy", model.CutUser, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable SP_PartTempStatus()
        {
            StoredProcedure sp = new StoredProcedure("SP_PartTempStatus");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetFileUpload()
        {
            StoredProcedure sp = new StoredProcedure("SP_GetFileUpload");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #region Start 7 june 2024  Assessment Controller 
        public static DataSet GetSPScoreMarkAnswer(string User, int FormId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScoreMarkAnswer");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int16);
            sp.Command.AddParameter("@User", User, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetQuestionSummaryMarks(string User, int FormId)
        {
            StoredProcedure sp = new StoredProcedure("SP_QuestionSummaryMarks");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int16);
            sp.Command.AddParameter("@User", User, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_ScorersSummaryMarks(string User, int FormId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScorersSummary");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int16);
            sp.Command.AddParameter("@User", User, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion
    }
}
