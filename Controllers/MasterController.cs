using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
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
        public JsonResult GetDistrictList(string SelectAll="1")
        {
            try
            {
                var data = CommonModel.GetDistrict(SelectAll);
                if (data != null)
                {
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);  //DO TO
                }
                return Json(new { IsSuccess = false, res = data }, JsonRequestBehavior.AllowGet); //DO TO
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);//DO TO
            }
        }

        #region Master
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
        public ActionResult GetTrainingCenterList(bool SelectAll,int DistrictId,int TrainingAgencyId)
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
        //public ActionResult GetBlckList(int DistrictId)
        //{
        //    try
        //    {
        //        var items = SP_Model.SPBlock(DistrictId);
        //        if (items != null)
        //        {
        //            var data = JsonConvert.SerializeObject(items);
        //            return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult GetYearList(int IsAll = 0)
        //{
        //    try
        //    {
        //        var items = CommonModel.GetYear(IsAll);
        //        if (items != null)
        //        {
        //            var data = JsonConvert.SerializeObject(items);
        //            return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult GetMonthList(int IsAll = 0)
        //{
        //    try
        //    {
        //        var items = CommonModel.GetMonth(IsAll);
        //        if (items != null)
        //        {
        //            var data = JsonConvert.SerializeObject(items);
        //            return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
        //    }
        //}
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