﻿using Hunarmis.Manager;
using Hunarmis.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Controllers
{
    [Authorize]
    public class MasterController : Controller
    {
        Hunar_DBEntities db = new Hunar_DBEntities();
        JsonResponseData response = new JsonResponseData();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserDetaillist()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        public ActionResult GetUserDetailData()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SPGetUserlist();
                if (tbllist != null)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        #region Master List
        public ActionResult GetStateList(bool SelectAll)
        {
            try
            {
                var items = CommonModel.GetState(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDistrictList(string SelectAll, int StateId)
        {
            try
            {
                var items = CommonModel.GetDistrict(SelectAll, StateId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBatchList(bool SelectAll)
        {
            try
            {
                var items = CommonModel.GetBatch(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTrainingAgencyList(bool SelectAll)
        {
            try
            {
                var items = CommonModel.GetTrainingAgency(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTrainingCenterList(bool SelectAll, int DistrictId, int TrainingAgencyId)
        {
            try
            {
                var items = CommonModel.GetTrainingCenter(SelectAll, DistrictId, TrainingAgencyId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetYearList(int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetYear(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMonthList(int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetMonth(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCallStatusList(bool SelectAll)
        {
            try
            {
                var items = CommonModel.GetEnumCallStatusList(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Create Master 
        public ActionResult GetBatchMasterllist()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_BatchList();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_BatchData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult BatchMaster(int id = 0)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            BatchModel model = new BatchModel();
            if (id > 0)
            {
                var tbl = db_.Batch_Master.Find(id);
                if (tbl != null)
                {
                    model.Id = tbl.Id;
                    model.BatchName = tbl.BatchName;
                    model.BatchStartDate = tbl.BatchStartDate;
                    model.BatchEndDate = tbl.BatchEndDate;
                }
            }
            return View(model);
        }
        //[AllowAnonymous]
        [HttpPost]
        //[EnableCors("*")]
        public JsonResult BatchMaster(BatchModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var tbl = model.Id != 0 ? db.Batch_Master.Find(model.Id) : new Batch_Master();
                if (tbl != null && model != null)
                {
                    tbl.BatchName = model.BatchName.Trim();
                    tbl.BatchStartDate = model.BatchStartDate;
                    tbl.BatchEndDate = model.BatchEndDate;
                    tbl.IsActive = true;
                    if (model.Id == 0)
                    {
                        if (!_db.Batch_Master.Any())
                        {
                            tbl.Id = 1;
                        }
                        else
                        {
                            var max = _db.Batch_Master?.Max(x => x.Id);
                            tbl.Id = max == 0 ? 1 : Convert.ToInt32(max.Value) + 1;

                        }
                        
                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        db.Batch_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.Updated = DateTime.Now;
                    }
                    int res = db.SaveChanges();
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

        public ActionResult GetCoursesMasterlist()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_CoursesList();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_CoursesData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult CoursesMaster(int id = 0)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            CoursesModel model = new CoursesModel();
            if (id > 0)
            {
                var tbl = db_.Courses_Master.Find(id);
                if (tbl != null)
                {
                    model.Id = tbl.Id;
                    model.CourseName = tbl.CourseName;
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult CoursesMaster(CoursesModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var tbl = model.Id != 0 ? db.Courses_Master.Find(model.Id) : new Courses_Master();
                if (tbl != null && model != null)
                {
                    tbl.CourseName = model.CourseName;
                    tbl.IsActive = true;
                    if (model.Id == 0)
                    {
                        if (!_db.Courses_Master.Any())
                        {
                            tbl.Id = 1;
                        }
                        else
                        {
                            var max = _db.Courses_Master?.Max(x => x.Id);
                            tbl.Id = max == 0 ? 1 : Convert.ToInt32(max.Value) + 1;

                        }

                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        db.Courses_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.Updated = DateTime.Now;
                    }
                    int res = db.SaveChanges();
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

        public ActionResult GetEducationalMasterlist()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_EducationalMList();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_EducationalData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult EducationalMaster(int id = 0)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            EducationalModel model = new EducationalModel();
            if (id > 0)
            {
                var tbl = db_.Educational_Master.Find(id);
                if (tbl != null)
                {
                    model.Id = tbl.Id;
                    model.QualificationName = tbl.QualificationName;
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult EducationalMaster(EducationalModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var tbl = model.Id != 0 ? db.Educational_Master.Find(model.Id) : new Educational_Master();
                if (tbl != null && model != null)
                {
                    tbl.QualificationName = model.QualificationName;
                    tbl.IsActive = true;
                    if (model.Id == 0)
                    {
                        if (!_db.Educational_Master.Any())
                        {
                            tbl.Id = 1;
                        }
                        else
                        {
                            var max = _db.Educational_Master?.Max(x => x.Id);
                            tbl.Id = max == 0 ? 1 : Convert.ToInt32(max.Value) + 1;

                        }

                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        db.Educational_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.Updated = DateTime.Now;
                    }
                    int res = db.SaveChanges();
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
        //---------------------//Training_Agency//----------------------
        public ActionResult GetTrainAgencylist()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_Training_AgencyList();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_TrainingAGYData", tbllist);// _TrainingAGYData 
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult AgencyMaster(int id = 0)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            TrainingAgencyModel model = new TrainingAgencyModel();
            if (id > 0)
            {
                var tbl = db_.TrainingAgency_Master.Find(id);
                if (tbl != null)
                {
                    model.Id = tbl.Id;
                    model.TrainingAgencyName = tbl.TrainingAgencyName;
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult AgencyMaster(TrainingAgencyModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var tbl = model.Id != 0 ? db.TrainingAgency_Master.Find(model.Id) : new TrainingAgency_Master();
                if (tbl != null && model != null)
                {
                    tbl.TrainingAgencyName = model.TrainingAgencyName;
                    tbl.IsActive = true;
                    if (model.Id == 0)
                    {
                        if (!_db.TrainingAgency_Master.Any())
                        {
                            tbl.Id = 1;
                        }
                        else
                        {
                            var max = _db.TrainingAgency_Master?.Max(x => x.Id);
                            tbl.Id = max == 0 ? 1 : Convert.ToInt32(max.Value) + 1;

                        }

                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        db.TrainingAgency_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.Updated = DateTime.Now;
                    }
                    int res = db.SaveChanges();
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
        //---------------------//Training_Centre//----------------------
        public ActionResult GetTrainingCentreMasterList()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_TrainingCentreList();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_TrainingCentreData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult TrainingCenter(int id = 0)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            TrainingCentreModel model = new TrainingCentreModel();
            if (id > 0)
            {
                var tbl = db_.TrainingCenter_Master.Find(id);
                if (tbl != null)
                {
                    model.Id = tbl.Id;
                    model.DistrictID = tbl.DistrictID_fk;
                    model.TrainingAgencyID = tbl.TrainingAgencyID_fk;
                    model.TrainingCenter = tbl.TrainingCenter;
                    model.Location = tbl.Location;
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult TrainingCenter(TrainingCentreModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var tbl = model.Id != 0 ? db.TrainingCenter_Master.Find(model.Id) : new TrainingCenter_Master();
                if (tbl != null && model != null)
                {
                    tbl.DistrictID_fk = model.DistrictID;
                    tbl.TrainingAgencyID_fk = model.TrainingAgencyID;
                    tbl.TrainingCenter = model.TrainingCenter;
                    tbl.Location = model.Location;
                    tbl.IsActive = true;
                    if (model.Id == 0)
                    {
                        if (!_db.TrainingCenter_Master.Any())
                        {
                            tbl.Id = 1;
                        }
                        else
                        {
                            var max = _db.TrainingCenter_Master?.Max(x => x.Id);
                            tbl.Id = max == 0 ? 1 : Convert.ToInt32(max.Value) + 1;

                        }

                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                        tbl.CreatedOn = DateTime.Now;
                        db.TrainingCenter_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                        tbl.Updated = DateTime.Now;
                    }
                    int res = db.SaveChanges();
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