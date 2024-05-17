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
    public class HomeController : Controller
    {
        public ActionResult Index()
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


        [AllowAnonymous]
        [HttpGet]
        public JsonResult SendMail()
        {
            string msg = "";
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            var tbllist = db_.tbl_MailData.Where(x => x.IsActive == true).ToList();
            string link = "https://forms.gle/qvNwLoPHgsTkDYCq6";
            int noofsend = 0;
            foreach (var item in tbllist)
            {
                noofsend++;
                msg += CommonModel.SendMail(item.EmailId,
                    Enums.GetEnumDescription(Enums.OptionMailSubject.SummativeAFD),
                    Enums.GetEnumDescription(Enums.OptionMailSubject.SAFDLink)
                    + " <a href=" + link + ">" +
                    Enums.GetEnumDescription(Enums.OptionMailSubject.SummativeAFD) + "</a> <br /><br /><br /><br /><br /><br /> "+" Thank & Regards",
                    "", item.Name, noofsend);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
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