using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
    [Authorize]
    [SessionCheck]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Dashboard()
        {

            return View();
        }

        public ActionResult GetIndex(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = SPManager.SP_Dashboard(model);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                    dt2 = ds.Tables[2];
                    dt3 = ds.Tables[3];
                    IsCheck = true;
                    var datares1 = JsonConvert.SerializeObject(dt);
                    var datares2 = JsonConvert.SerializeObject(dt1);
                    var datares3 = JsonConvert.SerializeObject(dt2);
                    var datares4 = JsonConvert.SerializeObject(dt3);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = datares1, Data2 = datares2, Data3 = datares3, Data4 = datares4 }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var datares = JsonConvert.SerializeObject(dt);
                var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound), resData = "" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult TrainingCenterWise()
        {
            return View();
        }
        
        public ActionResult CallingDashboard()
        {
            return View();
        }

        public ActionResult GetCallingDashboard(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = SPManager.Sp_DashboardPartCalling(model);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    //dt1 = ds.Tables[1];
                    //dt2 = ds.Tables[2];
                    //dt3 = ds.Tables[3];
                    IsCheck = true;
                    var datares1 = JsonConvert.SerializeObject(dt);
                    var datares2 = JsonConvert.SerializeObject(dt1);
                    var datares3 = JsonConvert.SerializeObject(dt2);
                    var datares4 = JsonConvert.SerializeObject(dt3);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = datares1, Data2 = datares2, Data3 = datares3, Data4 = datares4 }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var datares = JsonConvert.SerializeObject(dt);
                var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound), resData = "" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        #region
        public ActionResult CallingChart()
        {
            return View();
        }
        public ActionResult GetCallingChartMonth(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = SPManager.SP_CallChartWiseMonth(model);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                    dt2 = ds.Tables[2];
                    dt3 = ds.Tables[3];
                    IsCheck = true;
                    var datares1 = JsonConvert.SerializeObject(dt);
                    var datares2 = JsonConvert.SerializeObject(dt1);
                    var datares3 = JsonConvert.SerializeObject(dt2);
                    var datares4 = JsonConvert.SerializeObject(dt3);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = datares1, Data2 = datares2, Data3 = datares3, Data4 = datares4 }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var datares = JsonConvert.SerializeObject(dt);
                var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound), resData = "" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        public ActionResult CallStatus()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetCallStatus(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.sp_callstatus(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_CallStatusData", tbllist);
                    var resDt = JsonConvert.SerializeObject(tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html, resData = resDt }, JsonRequestBehavior.AllowGet);
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