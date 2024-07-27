using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Controllers
{
    [SessionCheck]
    [Authorize]
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }
        #region Attendance Form Submitted
        public ActionResult CreatedAttend() { return View(); }
        public ActionResult GetPartList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_GetParticipant(model);
                bool IsCheck = false; bool IsAttendance = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    IsAttendance = tbllist.Rows[0]["IsAttendance"].ToString() == "1" ? true : false;
                    html = ConvertViewToString("_ParticipantData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html, IsAttend = IsAttendance }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error.." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        [HttpPost]
        public JsonResult CreatedAttend(AttendanceModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            int res = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(model.Lat) || string.IsNullOrWhiteSpace(model.Long))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Allow to Location !!", Data = null };
                    var resResponseal = Json(response, JsonRequestBehavior.AllowGet);
                    resResponseal.MaxJsonLength = int.MaxValue;
                    return resResponseal;
                }
                if (string.IsNullOrWhiteSpace(model.StartDate.ToString()) || string.IsNullOrWhiteSpace(model.StrStartTime) || string.IsNullOrWhiteSpace(model.StrEndTime))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All fields are required !!", Data = null };
                    var resResponseal = Json(response, JsonRequestBehavior.AllowGet);
                    resResponseal.MaxJsonLength = int.MaxValue;
                    return resResponseal;
                }
                if (_db.tbl_AttendanceParticipant.Any(x => x.BatchId == model.BatchId && x.StartDate == model.StartDate))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Attendance is already submitted" + CommonModel.FormateDtDMY(model.StartDate.ToString()), Data = null };
                    var resResponseal = Json(response, JsonRequestBehavior.AllowGet);
                    resResponseal.MaxJsonLength = int.MaxValue;
                    return resResponseal;
                }
                var tbl = (model.AttendanceId_pk != null && model.AttendanceId_pk != Guid.Empty) ? _db.tbl_AttendanceParticipant.Find(model.AttendanceId_pk) : new tbl_AttendanceParticipant();
                //tbl = tbl == null ? new tbl_Mapped() : tbl;
                tbl_AttendParticipant tblattendpart;
                List<tbl_AttendParticipant> tblattendpartlist = new List<tbl_AttendParticipant>();
                tbl_AttendPartTopic tblattparttp;
                List<tbl_AttendPartTopic> tblattparttplist = new List<tbl_AttendPartTopic>();
                if (tbl != null && !string.IsNullOrWhiteSpace(model.Lat) && !string.IsNullOrWhiteSpace(model.Long))
                {
                    TimeSpan durst = CommonModel.GetTimeSpanValue(model.StrStartTime);
                    TimeSpan duret = CommonModel.GetTimeSpanValue(model.StrEndTime);
                    tbl.BatchId = model.BatchId;
                    tbl.CourseIds = model.CourseIds;
                    tbl.TopicesIds = model.TopicIds;
                    tbl.Lat = model.Lat;
                    tbl.Long = model.Long;
                    tbl.StartDate = model.StartDate;
                    tbl.StartTime = durst;
                    tbl.EndTime = duret;
                    tbl.IsActive = true;
                    if (model.AttendanceId_pk==Guid.Empty)
                    {
                        tbl.AttendanceId_pk = Guid.NewGuid();
                    }
                    var reslist = this.Request.Unvalidated.Form["PARTModel"];
                    if (reslist != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<AttendPartModel>>(reslist);
                        if (mlist != null)
                        {
                            if (mlist.Count > 0)
                            {
                                if (model.AttendanceId_pk != Guid.Empty)
                                {
                                    var tbldet = _db.tbl_AttendParticipant.Where(x => x.AttendanceId_fk == model.AttendanceId_pk).ToList();
                                    _db.tbl_AttendParticipant.RemoveRange(tbldet);
                                    _db.SaveChanges();
                                }
                                foreach (var item in mlist)
                                {
                                    if (item.ParticipantId_fk != Guid.Empty)
                                    {
                                        tblattendpart = new tbl_AttendParticipant();
                                        tblattendpart.AttendancePartId_pk = Guid.NewGuid();
                                        tblattendpart.AttendanceId_fk = tbl.AttendanceId_pk;
                                        tblattendpart.ParticipantId_fk = item.ParticipantId_fk;
                                        tblattendpart.AttendPartIsActive = true;
                                        tblattendpart.AttendPartCreatedBy = MvcApplication.CUser.UserId;
                                        tblattendpart.AttendPartCreatedOn = DateTime.Now;
                                        tblattendpartlist.Add(tblattendpart);
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(model.TopicIds))
                    {
                        //var atCourseIdssplt = model.CourseIds.Split(',');
                        var atpartsplt = model.TopicIds.Split(',');
                        if (atpartsplt.Length != 0)
                        {
                            if (model.AttendanceId_pk != Guid.Empty)
                            {
                                var tbldet = _db.tbl_AttendPartTopic.Where(x => x.AttendanceId_fk == model.AttendanceId_pk).ToList();
                                _db.tbl_AttendPartTopic.RemoveRange(tbldet);
                                _db.SaveChanges();
                            }
                            var SessionList = _db.SessionPlanTopic_Master.ToList();
                            foreach (var item in atpartsplt)
                            {
                                var sessiontopiceid = Convert.ToInt32(item);
                                var CourseId = SessionList.Where(x => x.SessionPlanTPId_pk == sessiontopiceid)?.FirstOrDefault().CourseId;
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    tblattparttp = new tbl_AttendPartTopic();
                                    tblattparttp.AttendanceTopicId_pk = Guid.NewGuid();
                                    tblattparttp.AttendanceId_fk = tbl.AttendanceId_pk;
                                    tblattparttp.CouresId = CourseId;
                                    tblattparttp.TopicId = sessiontopiceid;
                                    if (sessiontopiceid==261)
                                    {
                                        tblattparttp.TopicOther = model.TopicOther;
                                    }
                                    else
                                    {
                                        tblattparttp.TopicOther = null;
                                    }
                                    tblattparttp.AttendTopicIsActive = true;
                                    tblattparttp.AttendTopicCreatedBy = MvcApplication.CUser.UserId;
                                    tblattparttp.AttendTopicCreatedOn = DateTime.Now;
                                    tblattparttplist.Add(tblattparttp);
                                }
                            }

                        }
                    }
                    if (model.AttendanceId_pk == Guid.Empty)
                    {
                        
                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        _db.tbl_AttendanceParticipant.Add(tbl);
                        if (tblattendpartlist.Count > 0)
                        {
                            _db.tbl_AttendParticipant.AddRange(tblattendpartlist);
                        }
                        if (tblattparttplist.Count > 0)
                        {
                            _db.tbl_AttendPartTopic.AddRange(tblattparttplist);
                        }
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.UpdatedOn = DateTime.Now;
                        if (model.AttendanceId_pk != Guid.Empty)
                        {
                            if (tblattendpartlist.Count > 0)
                            {
                                _db.tbl_AttendParticipant.AddRange(tblattendpartlist);
                            }
                            if (tblattparttplist.Count > 0)
                            {
                                _db.tbl_AttendPartTopic.AddRange(tblattparttplist);
                            }
                        }
                    }
                    res += _db.SaveChanges();
                    if (res > 0)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Attendance Submitted Successfully!!!", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Data Not Submitted !!", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error..", Data = null };
            var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse4.MaxJsonLength = int.MaxValue;
            return resResponse4;
        }
        public ActionResult GetCalendarAttendanceList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_GetParticipant(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error.." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult AttendanceList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetAttendanceList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_AttendanceParticipantList(model);
                bool IsCheck = false; bool IsAttendance = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_AttendanceListParticipantData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html, IsAttend = IsAttendance }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error.." }, JsonRequestBehavior.AllowGet); throw;
            }
        }


        #endregion

        #region Assessment Schedule
        public ActionResult GetAssessmentSchedulellist(int BatchId = 0)
        {
            try
            {
                bool IsCheck = false;
                FilterModel model = new FilterModel();
                model.BatchId = BatchId.ToString();
                var tbllist = SPManager.SP_GetAssessmentScheduleList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AssessmentScheduleData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult AssessmentSchedule(Guid? id)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            AssessmentScheduleModel model = new AssessmentScheduleModel();
            if (id != Guid.Empty && id != null)
            {
                var tbl = db_.tbl_AssessmentSchedule.Find(id);
                if (tbl != null)
                {
                    model.AssessmentScheduleId_pk = tbl.AssessmentScheduleId_pk;
                    model.BatchId_fk = tbl.BatchId_fk;
                    model.Date = tbl.Date;
                    model.StrStartTime = CommonModel.GetTimeAMPM(tbl.StartTime.Value);
                    model.StrEndTime = CommonModel.GetTimeAMPM(tbl.EndTime.Value);
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult AssessmentSchedule(AssessmentScheduleModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (model.BatchId_fk == 0 || string.IsNullOrWhiteSpace(model.Date.ToString()) || string.IsNullOrWhiteSpace(model.StrStartTime) || string.IsNullOrWhiteSpace(model.StrEndTime))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All fields are required !!", Data = null };
                    var resResponseal = Json(response, JsonRequestBehavior.AllowGet);
                    resResponseal.MaxJsonLength = int.MaxValue;
                    return resResponseal;
                }
                if (_db.tbl_AssessmentSchedule.Any(x => x.BatchId_fk == model.BatchId_fk && x.Date == model.Date && model.AssessmentScheduleId_pk == Guid.Empty))
                {
                    var getbatch = _db.Batch_Master.Where(x => x.Id == model.BatchId_fk)?.FirstOrDefault().BatchName;
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Assessment Schedule is already submitted this Batch" + getbatch + " " + CommonModel.FormateDtDMY(model.Date.ToString()), Data = null };
                    var resResponseal = Json(response, JsonRequestBehavior.AllowGet);
                    resResponseal.MaxJsonLength = int.MaxValue;
                    return resResponseal;
                }
                if (_db.tbl_AssessmentSchedule.Any(x => x.BatchId_fk == model.BatchId_fk && model.AssessmentScheduleId_pk == Guid.Empty))
                {
                    var getbatch = _db.Batch_Master.Where(x => x.Id == model.BatchId_fk)?.FirstOrDefault().BatchName;
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Assessment Schedule is already submitted this Batch" + getbatch + " " + CommonModel.FormateDtDMY(model.Date.ToString()), Data = null };
                    var resResponseal = Json(response, JsonRequestBehavior.AllowGet);
                    resResponseal.MaxJsonLength = int.MaxValue;
                    return resResponseal;
                }
                //  DataTable dt = SPManager.SP_GetBatchForPart(TCenterIds);
                var tbl = model.AssessmentScheduleId_pk != Guid.Empty ? _db.tbl_AssessmentSchedule.Find(model.AssessmentScheduleId_pk) : new tbl_AssessmentSchedule();
                if (tbl != null && model != null)
                {
                    DateTime durst = DateTime.Parse(model.StrStartTime);
                    DateTime duret = DateTime.Parse(model.StrEndTime);
                    // TimeSpan durst = CommonModel.GetTimeSpanValue(model.StrStartTime);
                    // TimeSpan duret = CommonModel.GetTimeSpanValue(model.StrEndTime);
                    tbl.Date = model.Date;
                    tbl.StartTime = durst.TimeOfDay;
                    tbl.EndTime = duret.TimeOfDay;
                    tbl.AssessmentSchedule = false;
                    tbl.IsActive = true;

                    if (model.AssessmentScheduleId_pk == Guid.Empty)
                    {
                        tbl.AssessmentScheduleId_pk = Guid.NewGuid();
                        FilterModel filterModel = new FilterModel();
                        filterModel.BatchId = model.BatchId_fk.Value.ToString();
                        DataTable tcid = SPManager.SP_GetParticipant(filterModel).AsEnumerable().CopyToDataTable();
                        tbl.TrainingCenterId_fk = Convert.ToInt32(tcid.Rows[0]["TrainingCenterId"].ToString());
                        tbl.CourseId_fk = Convert.ToInt32(tcid.Rows[0]["CourseId"].ToString());
                        tbl.BatchId_fk = model.BatchId_fk;
                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        _db.tbl_AssessmentSchedule.Add(tbl);
                        // var resmail = CommonModel.SendMailForParticipants(tbl.BatchId_fk.ToString());
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.UpdatedOn = DateTime.Now;
                    }
                    int res = _db.SaveChanges();
                    if (res > 0)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Record Submitted Successfully!!!", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                    }
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error..", Data = null };
            var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse4.MaxJsonLength = int.MaxValue;
            return resResponse4;
        }
        #endregion


        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

    }
}