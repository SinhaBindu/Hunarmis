using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Hunarmis.Manager;
using Hunarmis.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
//using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Controllers
{
    [Authorize]
    public class ParticipantController : Controller
    {
        Hunar_DBEntities db = new Hunar_DBEntities();
        int results = 0; int results2nd = 0;

        public ActionResult Index()
        {
            //var physicalFilePath = Server.MapPath("~/upload/template.xlsx");
            //ExcelUtility excelut = new ExcelUtility();
            //DataTable dt = excelut.GetData(physicalFilePath);
            return View();
        }
        public ActionResult ParticipantList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPartList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_ParticipantList(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_PartData", tbllist);
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
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult AddParticipant(Guid? Id)
        {
            ParticipantModel model = new ParticipantModel();
            if (Id != Guid.Empty && Id != null)
            {
                var tbl = db.tbl_Participant.Find(Id);
                if (tbl != null)
                {
                    model.ID = tbl.ID;
                    model.RegID = tbl.RegID;
                    model.FirstName = tbl.FirstName;
                    model.MiddleName = tbl.MiddleName;
                    model.LastName = tbl.LastName;
                    model.Gender = tbl.Gender;
                    model.Age = tbl.Age;
                    model.AadharCardNo = tbl.AadharCardNo;
                    model.EmailID = tbl.EmailID;
                    model.PhoneNo = tbl.PhoneNo;
                    model.AlternatePhoneNo = tbl.AlternatePhoneNo;
                    model.AssessmentScore = tbl.AssessmentScore;
                    model.BatchId = tbl.BatchId;
                    model.EduQualificationID = tbl.EduQualificationID;
                    model.EduQualOther = tbl.EduQualOther;
                    model.CourseEnrolledID = tbl.CourseEnrolledID;
                    model.StateID = tbl.StateID;
                    model.DistrictID = tbl.DistrictID;
                    model.TrainingAgencyID = tbl.TrainingAgencyID;
                    model.TrainingCenterID = tbl.TrainingCenterID;
                    model.TrainerName = tbl.TrainerName;
                    model.IsOffered = tbl.IsOffered;
                    model.IsPlaced = tbl.IsPlaced.Value;
                }
            }
            return View(model);
        }
        [HttpPost]
        //[EnableCors("*")]
        public ActionResult AddParticipant(ParticipantModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    var getdt = db_.tbl_Participant.Where(x => x.IsActive == true).ToList();
                    if (getdt.Any(x => x.PhoneNo == model.PhoneNo.Trim() && x.BatchId == model.BatchId && model.ID == Guid.Empty))
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Registration.<br /> <span> Reg ID : <strong> " + getdt?.FirstOrDefault().RegID + " </strong>  </span>", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                    var tbl = model.ID != Guid.Empty ? db.tbl_Participant.Find(model.ID) : new tbl_Participant();
                    if (tbl != null)
                    {
                        tbl.FirstName = !(string.IsNullOrWhiteSpace(model.FirstName)) ? model.FirstName.Trim() : model.FirstName;
                        tbl.MiddleName = !(string.IsNullOrWhiteSpace(model.MiddleName)) ? model.MiddleName.Trim() : model.MiddleName;
                        tbl.LastName = !(string.IsNullOrWhiteSpace(model.LastName)) ? model.LastName.Trim() : model.LastName;
                        //  tbl.StateId = db.State_Mast.Where(x => x.ID == sid).FirstOrDefault().ID.ToString();
                        tbl.Gender = !(string.IsNullOrWhiteSpace(model.Gender)) ? model.Gender.Trim() : model.Gender;
                        tbl.Age = !(string.IsNullOrWhiteSpace(model.Age)) ? model.Age.Trim() : model.Age;
                        tbl.PhoneNo = !(string.IsNullOrWhiteSpace(model.PhoneNo)) ? model.PhoneNo.Trim() : model.PhoneNo;
                        tbl.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(model.AlternatePhoneNo)) ? model.AlternatePhoneNo.Trim() : model.AlternatePhoneNo;
                        tbl.EmailID = !(string.IsNullOrWhiteSpace(model.EmailID)) ? model.EmailID.Trim() : model.EmailID;
                        tbl.AadharCardNo = !(string.IsNullOrWhiteSpace(model.AadharCardNo)) ? model.AadharCardNo.Trim() : model.AadharCardNo;
                        tbl.AssessmentScore = !(string.IsNullOrWhiteSpace(model.AssessmentScore)) ? model.AssessmentScore.Trim() : model.AssessmentScore;
                        tbl.BatchId = model.BatchId;
                        tbl.EduQualificationID = model.EduQualificationID;
                        tbl.EduQualOther = model.EduQualificationID == 4 ? model.EduQualOther : null;
                        tbl.CourseEnrolledID = model.CourseEnrolledID;
                        tbl.StateID = model.StateID;
                        tbl.DistrictID = model.DistrictID;
                        tbl.TrainingAgencyID = model.TrainingAgencyID;
                        tbl.TrainingCenterID = model.TrainingCenterID;
                        tbl.TrainerName = !(string.IsNullOrWhiteSpace(model.TrainerName)) ? model.TrainerName.Trim() : model.TrainerName;
                        tbl.IsActive = true;
                        tbl.IsPlaced = model.IsPlaced;
                        tbl.IsOffered = model.IsOffered;
                        if (model.ID == Guid.Empty)
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                tbl.CreatedBy = MvcApplication.CUser.UserId;
                            }
                            tbl.ID = Guid.NewGuid();
                            tbl.CreatedOn = DateTime.Now;
                            tbl.CallTempStatus = (int)Enums.eTempCallStatus.CallNot;
                            //tbl.FixedCallLimitMonth = model.IsPlaced == true ? (int)Enums.eCallLimitDuration.FixedCallLimit : 0;
                            // tbl.AtPresentCallStatus = model.IsPlaced == true ? 0 : 0;
                            tbl.FixedCallLimitMonth = (int)Enums.eCallLimitDuration.FixedCallLimit;
                            tbl.AtPresentCallStatus = 0;

                            db.tbl_Participant.Add(tbl);
                        }
                        else
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                tbl.UpdatedBy = MvcApplication.CUser.UserId;
                            }
                            tbl.UpdatedOn = DateTime.Now;
                        }
                        results = db.SaveChanges();
                    }
                    if (results > 0)
                    {
                        // var taskres = CU_RegLogin(model, tbl.ID);
                        var reg_id = db_.tbl_Participant.Where(x => x.PhoneNo == model.PhoneNo.Trim() && x.EmailID == model.EmailID.Trim())?.FirstOrDefault().RegID; //if (case_id != null) { }
                        if (model.ID != Guid.Empty)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully registered! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                };
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            return View();
        }

        #region Calling Method Post Data
        public ActionResult GetTempStatus()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_PartTempStatus();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var resdata = JsonConvert.SerializeObject(tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = resdata }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult ParticipantCallList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPartCallList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_ParticipantList(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_PartCallData", tbllist);
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
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult AddPartCalling(Guid? Id, Guid? PartId)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            ParticipantChildModel model = new ParticipantChildModel();
            if (PartId != Guid.Empty && PartId != null)
            {
                if (PartId != Guid.Empty)
                {
                    var tbl = db_.tbl_Participant.Find(PartId);
                    if (tbl != null)
                    {
                        tbl.CallTempStatus = (int)Enums.eTempCallStatus.CallOnProgress;
                        db_.SaveChanges();
                    }
                }
                model.ParticipantId_fk = PartId;
                var getdt = db_.tbl_Participant.Where(x => x.IsActive == true && x.ID == PartId)?.FirstOrDefault();
                if (getdt != null)
                {
                    model.RegID = getdt.RegID;
                    model.FullName = getdt.FirstName + " " + getdt.MiddleName + " " + getdt.LastName;
                    var batch = db_.Batch_Master.Where(x => x.Id == getdt.BatchId)?.FirstOrDefault();
                    model.BatchName = batch != null ? batch.BatchName : null;
                    model.SBatchDt = batch != null ? CommonModel.FormateDtDMY(batch.BatchStartDate.ToString()) : null;
                    model.EBatchDt = batch != null ? CommonModel.FormateDtDMY(batch.BatchEndDate.ToString()) : null;
                }
            }
            if (Id != Guid.Empty && Id != null)
            {
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddPartCalling(ParticipantChildModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (model != null)
                {
                    var getdt = db_.tbl_Participant.Where(x => x.IsActive == true && x.ID == model.ParticipantId_fk)?.FirstOrDefault();
                    if (model.CallType == Enums.GetEnumDescription(Enums.eTypeCall.No)
                        && !string.IsNullOrWhiteSpace(model.Remark))
                    {
                        if (db_.tbl_Participant_Calling.Any(x => x.ParticipantId_fk == model.ParticipantId_fk && model.ID == Guid.Empty && (x.QuesMonth == model.QuesMonth && x.QuesYear == model.QuesYear)))
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "This Participant Is Already Exists.<br /> <span> Reg ID : <strong>" + getdt.RegID + " </strong>  </span>", Data = null };
                            var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse1.MaxJsonLength = int.MaxValue;
                            return resResponse1;
                        }
                        //var tbl = new tbl_Participant_Calling();
                        if (model.ID == Guid.Empty)
                        {
                            //tbl.ID = Guid.NewGuid();
                            //tbl.QuesMonth = model.QuesMonth;
                            //tbl.QuesYear = model.QuesYear;
                            //tbl.IsCalling = model.CallType;
                            //tbl.Remark = model.Remark.Trim();
                            //tbl.IsActive = true;
                            //tbl.CreatedBy = MvcApplication.CUser.UserId;
                            //tbl.CreatedOn = DateTime.Now;
                            //db_.tbl_Participant_Calling.Add(tbl);

                            var tblpart = db_.tbl_Participant.Find(model.ParticipantId_fk);
                            tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallNotPick;
                            tblpart.CallYear = model.QuesYear;
                            tblpart.CallMonth = model.QuesMonth;
                            tblpart.CallRemark = model.Remark.Trim();

                            results = db_.SaveChanges();
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Congratulations, you have been Submitted successfully Reg ID : <strong>" + getdt.RegID + " </strong>  </span>", Data = null };
                            var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse1.MaxJsonLength = int.MaxValue;
                            return resResponse1;
                        }
                    }
                    else if (model.CallType == Enums.GetEnumDescription(Enums.eTypeCall.Yes))
                    {
                        if (ModelState.IsValid)
                        {
                            if (db_.tbl_Participant_Calling.Any(x => x.ParticipantId_fk == model.ParticipantId_fk && model.ID == Guid.Empty && (x.QuesMonth == model.QuesMonth && x.QuesYear == model.QuesYear)))
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "This Participant Is Already Exists.<br /> <span> Reg ID : <strong>" + getdt.RegID + " </strong>  </span>", Data = null };
                                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse1.MaxJsonLength = int.MaxValue;
                                return resResponse1;
                            }
                            var tbl = model.ID != Guid.Empty ? db.tbl_Participant_Calling.Find(model.ID) : new tbl_Participant_Calling();
                            if (tbl != null)
                            {
                                tbl.IsAssessmentCert = model.IsAssessmentCert;
                                tbl.IsPresent = model.IsPresent;
                                //tbl.IsPresent = model.IsPresent.ToString() == Enums.GetEnumDescription(Enums.eTypeCall.Yes) ? model.IsPresent : model.IsPresent;
                                if (model.IsPresent == Enums.GetEnumDescription(Enums.eTypeCall.Yes))
                                {
                                    tbl.IsComfortable = model.IsComfortable;
                                    tbl.EmpCompany = model.EmpCompany;
                                    tbl.FirstJobTraining = model.FirstJobTraining;
                                    tbl.DOJ = model.DOJ;
                                    tbl.CurrentEmployer = model.CurrentEmployer;
                                    tbl.Designation = model.Designation;
                                    tbl.SalaryPackage = model.SalaryPackage;
                                    tbl.CurrentlyWorking = model.CurrentlyWorking;
                                    tbl.WorkingKM = model.WorkingKM;
                                    tbl.WorkingKMOther = model.WorkingKMOther;
                                    tbl.Traininghelp = model.Traininghelp;
                                    tbl.SalaryWages = model.SalaryWages;
                                    tbl.ExpectationJobRole = model.ExpectationJobRole;
                                    tbl.WorkPlaceSafe = model.WorkPlaceSafe;
                                    tbl.IsMSBenefit = model.IsMSBenefit;
                                    tbl.MSBenefitId = model.MSBenefitId;
                                    tbl.MSOther = model.MSOther;
                                    tbl.AnyChallenges = model.AnyChallenges;
                                    tbl.AreaSupport = model.AreaSupport;
                                }
                                tbl.EmployedId = model.IsPresent.ToString() == Enums.GetEnumDescription(Enums.eTypeCall.Yes) ? model.EmployedId : null;
                                tbl.EmployedOther = model.EmployedId == 3 ? model.EmployedOther : null;
                                tbl.IsGettingjob = model.IsGettingjob;
                                tbl.PlacedStatus = model.PlacedStatus;
                                tbl.IsActive = true;
                                if (model.ID == Guid.Empty)
                                {
                                    tbl.ID = Guid.NewGuid();
                                    tbl.QuesMonth = model.QuesMonth;
                                    tbl.QuesYear = model.QuesYear;
                                    tbl.CallingDate = DateTime.Now.Date;
                                    tbl.IsCalling = Enums.GetEnumDescription(Enums.eTypeCall.Yes);
                                    tbl.ParticipantId_fk = model.ParticipantId_fk;
                                    tbl.CreatedBy = MvcApplication.CUser.UserId;
                                    tbl.CreatedOn = DateTime.Now;
                                    db_.tbl_Participant_Calling.Add(tbl);
                                    results += db_.SaveChanges();
                                    var tblpart = db_.tbl_Participant.Find(model.ParticipantId_fk);
                                    var tblcal = db_.tbl_Participant_Calling.Where(x => x.ParticipantId_fk == model.ParticipantId_fk);
                                    var noofcall = tblcal != null ? tblcal.Count() + 1 : 0;
                                    tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallDone;
                                    tblpart.IsPlaced = model.PlacedStatus == Enums.GetEnumDescription(Enums.ePlaced.Yes) ? true : false;
                                    tblpart.CallMonth = model.QuesMonth;
                                    tblpart.CallYear = model.QuesYear;
                                    tblpart.AtPresentCallStatus = noofcall;
                                    results += db_.SaveChanges();
                                }
                                else if (model.ID != Guid.Empty)
                                {
                                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                                    tbl.UpdatedOn = DateTime.Now;
                                    results += db_.SaveChanges();
                                }
                                if (results > 0)
                                {
                                    var reg_id = db_.tbl_Participant.Where(x => x.ID == model.ParticipantId_fk)?.FirstOrDefault().RegID; //if (case_id != null) { }
                                    if (model.ID != Guid.Empty)
                                    {
                                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                        resResponse3.MaxJsonLength = int.MaxValue;
                                        return resResponse3;
                                    }
                                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully Questionnaire! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse1.MaxJsonLength = int.MaxValue;
                                    return resResponse1;
                                }
                            }
                        }
                        else
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                            var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse1.MaxJsonLength = int.MaxValue;
                            return resResponse1;
                        };
                    };
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !", Data = null };
            var resResponse = Json(response, JsonRequestBehavior.AllowGet);
            resResponse.MaxJsonLength = int.MaxValue;
            return resResponse;
        }
        #endregion

        public ActionResult PartQuestionList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPartQuestionList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_PartQuesList(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_PartQuesData", tbllist);
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
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult QuesResponse(string PartQuestId, string PartId, int M, int Y)
        {
            FilterModel model = new FilterModel();
            model.ParticipantQuestionId = PartQuestId;
            //model.YearId = Y;
            //model.MonthId = M;
            model.ParticipantId = PartId;
            DataTable dt = SPManager.SP_PartQuesList(model);
            return View(dt);
        }
        #region File Excel Upload Future 
        public ActionResult ExcelFileUpload()
        {
            FileExcelModel model = new FileExcelModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ExcelFileUpload(FileExcelModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            var strmobile = "";
            if (model != null)
            {
                try
                {
                    if (ModelState.IsValid && model.FileUpload != null)
                    {
                        if (db_.tbl_FileUpload.Any(x => x.FileName == model.FileName.Trim()))
                        {
                            var res = Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.Already) }, JsonRequestBehavior.AllowGet);
                            res.MaxJsonLength = int.MaxValue;
                            return res;
                        }
                        var maxid = db_.tbl_FileUpload.Count() != 0 ? db_.tbl_FileUpload?.Max(x => x.Id) : 0;
                        var lastaddmaxid = maxid == 0 ? 1 : maxid + 1;
                        var filePath = CommonModel.SaveSingleExcelFile(model.FileUpload, lastaddmaxid.ToString(), "ParticipantFile");
                        var physicalFilePath = Server.MapPath(filePath);
                        ExcelUtility excelut = new ExcelUtility();
                        DataTable dt = excelut.GetData(physicalFilePath);
                        if (dt.Rows.Count > 0)
                        {
                            tbl_FileUpload tbl = new tbl_FileUpload();
                            tbl.UploadDate = model.UploadDate;
                            tbl.FileName = model.FileName.Trim();
                            tbl.FilePath = filePath;
                            tbl.CreatedBy = MvcApplication.CUser.UserId;
                            tbl.CreatedOn = DateTime.Now;
                            tbl.IsActive = true;
                            db.tbl_FileUpload.Add(tbl);

                            /* Inserted Data */
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString()))
                                {
                                    var moblie = dr["PhoneNo"].ToString();
                                    if (db_.tbl_Participant.Any(x => x.PhoneNo == moblie))
                                    {
                                        strmobile += "," + dr["PhoneNo"].ToString() + Enums.GetEnumDescription(Enums.eReturnReg.Already);
                                    }
                                    else
                                    {
                                        var tblpart = new tbl_Participant();
                                        if (tblpart != null)
                                        {
                                            var fulllist = dr["Name"].ToString().Split(' ');
                                            if (fulllist != null || fulllist.Length != 0)
                                            {
                                                if (fulllist.Length == 2)
                                                {
                                                    tblpart.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                    tblpart.LastName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                }
                                                else if (fulllist.Length == 3)
                                                {
                                                    tblpart.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                    tblpart.MiddleName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                    tblpart.LastName = !(string.IsNullOrWhiteSpace(fulllist[2])) ? fulllist[2].Trim().ToUpper() : null;
                                                }
                                            }
                                            
                                            var sn = Convert.ToString(dr["StateName"]);
                                            var StateId = db.State_Master.Where(x => x.StateName == sn).FirstOrDefault()?.Id;
                                            tblpart.StateID = StateId;
                                            var dname = Convert.ToString(dr["DistrictName"]);
                                            var DistrictID = db.District_Master.Where(x => x.DistrictName == dname && x.StateId == StateId)?.FirstOrDefault().Id;
                                            tblpart.DistrictID = DistrictID;

                                            tblpart.Gender = !(string.IsNullOrWhiteSpace(dr["Gender"].ToString())) ? dr["Gender"].ToString().Trim() : null;
                                            tblpart.Age = !(string.IsNullOrWhiteSpace(dr["Age"].ToString())) ? dr["Age"].ToString().Trim() : null;
                                            tblpart.PhoneNo = !(string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString())) ? dr["PhoneNo"].ToString().Trim() : null;
                                            // tblpart.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(dr["AlternatePhoneNo"].ToString())) ? dr["AlternatePhoneNo"].ToString().Trim() : null;
                                            tblpart.EmailID = !(string.IsNullOrWhiteSpace(dr["EmailID"].ToString())) ? dr["EmailID"].ToString().Trim() : null;
                                            tblpart.AadharCardNo = !(string.IsNullOrWhiteSpace(dr["AadharCardNo"].ToString())) ? dr["AadharCardNo"].ToString().Trim() : null;
                                            tblpart.AssessmentScore = !(string.IsNullOrWhiteSpace(dr["AssessmentScore"].ToString())) ? dr["AssessmentScore"].ToString().Trim() : null;

                                            var bName = Convert.ToString(dr["BatchName"]);
                                            var BatchId = !(string.IsNullOrWhiteSpace(bName)) ? db.Batch_Master.Where(x => x.BatchName == bName).FirstOrDefault()?.Id : null;
                                            tblpart.BatchId = BatchId;
                                            var qName = Convert.ToString(dr["QualificationName"]);
                                            var EducationId = !(string.IsNullOrWhiteSpace(qName)) ? db.Educational_Master.Where(x => x.QualificationName == qName).FirstOrDefault()?.Id : null;
                                            tblpart.EduQualificationID = EducationId;
                                            tblpart.EduQualOther = EducationId == 4 && !(string.IsNullOrWhiteSpace(dr["EduQualOther"].ToString())) ? dr["EduQualOther"].ToString().Trim() : "NA";
                                            var cName = Convert.ToString(dr["CourseName"]);
                                            var CourseEnrolledId = !(string.IsNullOrWhiteSpace(cName)) ? db.Courses_Master.Where(x => x.CourseName == cName).FirstOrDefault()?.Id : null;
                                            tblpart.CourseEnrolledID = CourseEnrolledId;

                                            var trainingAgencyName = Convert.ToString(dr["TrainingAgencyName"]);
                                            var TrainingAgencyId = !(string.IsNullOrWhiteSpace(trainingAgencyName)) ? db.TrainingAgency_Master.Where(x => x.TrainingAgencyName == trainingAgencyName).FirstOrDefault()?.Id : null;
                                            tblpart.TrainingAgencyID = TrainingAgencyId;
                                            var trainingCenter = Convert.ToString(dr["TrainingCenter"]);
                                            var TrainingCenterId = !(string.IsNullOrWhiteSpace(trainingCenter)) ? db.TrainingCenter_Master.Where(x => x.TrainingCenter == trainingCenter).FirstOrDefault()?.Id : null;
                                            tblpart.TrainingCenterID = TrainingCenterId;
                                            tblpart.TrainerName = !(string.IsNullOrWhiteSpace(dr["TrainerName"].ToString())) ? dr["TrainerName"].ToString().Trim() : null;
                                            //if (dr["DOB"].ToString()!="")
                                            //{
                                            //    tblpart.DOB =Convert.ToDateTime(dr["DOB"].ToString());
                                            //}
                                            tblpart.IsActive = true;
                                            if (tblpart.ID == Guid.Empty)
                                            {
                                                if (User.Identity.IsAuthenticated)
                                                {
                                                    tblpart.CreatedBy = MvcApplication.CUser.UserId;
                                                }
                                                tblpart.ID = Guid.NewGuid();
                                                tblpart.CreatedOn = DateTime.Now;
                                                tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallNot;
                                                tblpart.IsPlaced = !(string.IsNullOrWhiteSpace(dr["IsPlaced"].ToString())) && dr["IsPlaced"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;
                                                //tblpart.FixedCallLimitMonth = tblpart.IsPlaced == true ? (int)Enums.eCallLimitDuration.FixedCallLimit : 0;
                                               // tblpart.FixedCallLimitMonth = tblpart.IsPlaced == true ? (int)Enums.eCallLimitDuration.FixedCallLimit : 0;
                                                tblpart.FixedCallLimitMonth = (int)Enums.eCallLimitDuration.FixedCallLimit;
                                                tblpart.AtPresentCallStatus = 0;
                                                tblpart.IsOffered = !(string.IsNullOrWhiteSpace(dr["IsOffered"].ToString())) && dr["IsOffered"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;
                                                db.tbl_Participant.Add(tblpart);
                                            }
                                            results += db.SaveChanges();
                                        }
                                    }
                                }


                            }
                            if (results > 0)
                            {
                                var res = Json(new { IsSuccess = true, Message = Enums.GetEnumDescription(Enums.eReturnReg.Insert) + strmobile }, JsonRequestBehavior.AllowGet);
                                res.MaxJsonLength = int.MaxValue;
                                return res;
                            }
                        }
                    }
                    else
                    {
                        var res = Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) }, JsonRequestBehavior.AllowGet);
                        res.MaxJsonLength = int.MaxValue;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    string er = ex.Message;
                    return Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.Error) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GeExcelFileUploadList()
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_GetFileUpload();
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_ExcelData", tbllist);
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
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
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