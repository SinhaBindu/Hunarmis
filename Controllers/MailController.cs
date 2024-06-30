using Hunarmis.Manager;
using Hunarmis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Hunarmis.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        #region Mail send For Participant
        [AllowAnonymous]
        [HttpGet]
        public JsonResult SendMailForParticipant(string BatchId = "")
        {
            var Ischeck = false;var Message = "";
            var res = CommonModel.SendMailForParticipants(BatchId);
            Ischeck = res==-1 || res==-2?false:true;
            Message = res==-1 || res==-2?"Not Mail":"Mail Send Successfully !! "+res;
            return Json(new { IsSuccess = Ischeck, resData = Message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //[AllowAnonymous]
        //[HttpGet]
        //public JsonResult SendMail()
        //{
        //    string msg = "";
        //    Hunar_DBEntities db_ = new Hunar_DBEntities();
        //    var tbllist = db_.tbl_MailData.Where(x => x.IsActive == true).ToList();
        //    string link = "https://forms.gle/qvNwLoPHgsTkDYCq6";
        //    int noofsend = 0;
        //    foreach (var item in tbllist)
        //    {
        //        noofsend++;
        //        msg += CommonModel.SendMail(item.EmailId,
        //            Enums.GetEnumDescription(Enums.OptionMailSubject.SummativeAFD),
        //            Enums.GetEnumDescription(Enums.OptionMailSubject.SAFDLink)
        //            + " <a href=" + link + ">" +
        //            Enums.GetEnumDescription(Enums.OptionMailSubject.SummativeAFD) + "</a> <br /><br /><br /><br /><br /><br /> " + " Thank & Regards",
        //            "", item.Name, noofsend);
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

    }
}