﻿using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
//using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Data;
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
                        if (model.ID == Guid.Empty)
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                tbl.CreatedBy = MvcApplication.CUser.UserId;
                            }
                            tbl.ID = Guid.NewGuid();
                            tbl.CreatedOn = DateTime.Now;
                            tbl.CallTempStatus = (int)Enums.eTempCallStatus.CallNot;
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
                                tbl.IsPresent = model.IsPresent.ToString() == Enums.GetEnumDescription(Enums.eTypeCall.Yes) ? model.IsPresent : null;
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
                                    db.tbl_Participant_Calling.Add(tbl);
                                    var tblpart = db_.tbl_Participant.Find(model.ParticipantId_fk);
                                    tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallDone;
                                    tblpart.IsPlaced = model.PlacedStatus == Enums.GetEnumDescription(Enums.ePlaced.Yes) ? true : false;
                                    db_.SaveChanges();
                                }
                                else if (model.ID != Guid.Empty)
                                {
                                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                                    tbl.UpdatedOn = DateTime.Now;
                                }
                                results = db.SaveChanges();
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
        public ActionResult QuesResponse(string PartQuestId, int M, int Y)
        {
            FilterModel model = new FilterModel();
            model.ParticipantQuestionId = PartQuestId;
            model.YearId = Y;
            model.MonthId = M;
            model.ParticipantQuestionId = PartQuestId;
            DataTable dt = SPManager.SP_PartQuesList(model);
            return View(dt);
        }
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