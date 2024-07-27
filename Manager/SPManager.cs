using DocumentFormat.OpenXml.EMMA;
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
        public static DataTable SPGetUserlist(int? RoleId)
        {
            StoredProcedure sp = new StoredProcedure("SPGetUserlist");
            sp.Command.AddParameter("@RoleId", RoleId, DbType.Int32);
            sp.Command.AddParameter("@User", HttpContext.Current.User.Identity.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetDTACMasterList(int DistrictId = 0, int TAgencyId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetDTACMasterList");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@TAgencyId", TAgencyId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetTrainer1CenterattimeList(int TrainingCenterId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetTrainer");
            sp.Command.AddParameter("@TrainingCenterId", TrainingCenterId.ToString(), DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetAllTrainerList(string TrainerId = "")
        {
            StoredProcedure sp = new StoredProcedure("SP_GetAllTrainerList");
            sp.Command.AddParameter("@TrainerId", TrainerId.ToString(), DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetBatchForPart(string TrainerId, string TCIds, int BatchId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetBatchForPart");
            sp.Command.AddParameter("@TrainerId", TrainerId, DbType.String);
            sp.Command.AddParameter("@TCIds", TCIds, DbType.String);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetModuleWiseBatches(int ModuleType = 0,int CId=0, string TrainerId="", string TCIds="", int BatchId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetModuleWiseBatches");
            sp.Command.AddParameter("@TrainerId", TrainerId, DbType.String);
            sp.Command.AddParameter("@TCIds", TCIds, DbType.String);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@CourseId", CId, DbType.Int32);
            sp.Command.AddParameter("@ModuleType", ModuleType, DbType.Int32);
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
            //model.CallStatus = !string.IsNullOrEmpty(model.CallStatus) ? model.CallStatus :"";
            model.UserId = string.IsNullOrEmpty(model.UserId) || model.UserId == "-1" || model.UserId == "0" ? "" : model.UserId;
            model.CallStatus = string.IsNullOrEmpty(model.CallStatus) || model.CallStatus == "-1" ? "" : model.CallStatus;
            StoredProcedure sp = new StoredProcedure("SP_ParticipantList");
            sp.Command.AddParameter("@Type", model.Type, DbType.Int32);
            sp.Command.AddParameter("@Search", model.Search, DbType.String);
            sp.Command.AddParameter("@CallStatus", model.CallStatus, DbType.String);
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            sp.Command.AddParameter("@CourseId", model.CourseId, DbType.String);
            sp.Command.AddParameter("@UserId", model.UserId, DbType.String);
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
        public static DataSet GetSPScoreMarkAnswer(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScoreMarkAnswer");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetQuestionSummaryMarks(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_QuestionSummaryMarks");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_ScorersSummaryMarks(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScorersSummary");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion
        #region Attendance Module
        public static DataTable SP_GetParticipant(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetParticipant");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_AttendanceParticipantList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AttendanceParticipantList");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetAssessmentScheduleList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetAssessmentScheduleList");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            sp.Command.AddParameter("@AssessmentScheduleId", model.AssessmentScheduleId, DbType.String);
            sp.Command.AddParameter("@TrainingCenterIds", MvcApplication.CUser.MappedTCenterIds, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion
        #region Login For Participant
        public static DataTable SP_LoginForParticipantCheck(ParticipantLoginModel model)
        {
            var ParticipantId_fk = model.ParticipantId_fk != Guid.Empty ? model.ParticipantId_fk.ToString() : "";
            StoredProcedure sp = new StoredProcedure("SP_LoginForParticipantCheck");
            sp.Command.AddParameter("@EmailID", model.EmailID, DbType.String);
            sp.Command.AddParameter("@Password", model.Password, DbType.String);
            sp.Command.AddParameter("@RandomValue", model.RandomValue, DbType.String);
            sp.Command.AddParameter("@ParticiPantId", ParticipantId_fk, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion
        #region Mail send For Participant Email Id
        public static DataTable SP_MailSendParticipantWise(string BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_MailSendParticipantWise");
            sp.Command.AddParameter("@BatchId", BatchId, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        #endregion

        #region Report
        public static DataTable sp_callstatus(FilterModel model)
        {
            //FromDt = !string.IsNullOrWhiteSpace(FromDt) ? FromDt:"";
            StoredProcedure sp = new StoredProcedure("sp_callstatus");
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            sp.Command.AddParameter("@ReportedBy", model.UserId, DbType.String);
            sp.Command.AddParameter("@SD", model.FromDt, DbType.String);
            sp.Command.AddParameter("@ED", model.ToDt, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_CourseWiseTopices(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CourseWiseTopices");
            sp.Command.AddParameter("@CourseId", model.CourseId, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_BatchWiseCourse(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CourseBatch");
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion

    }
}
