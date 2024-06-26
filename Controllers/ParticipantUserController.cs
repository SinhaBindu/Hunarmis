using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Hunarmis.Manager;
using Hunarmis.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Hunarmis.Manager.CommonModel;

namespace Hunarmis.Controllers
{
    //[Authorize(Roles = "User")]
    
    public class ParticipantUserController : Controller
    {
        // GET: ParticipantUser
        Hunar_DBEntities db = new Hunar_DBEntities();
        [SessionCheckPart]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string RandomValue)
        {
            Session.Clear();
            if (!string.IsNullOrWhiteSpace(RandomValue))
            {
                Session["RV"] = RandomValue;
            }
            else
            {
                Session["RV"] = null;
            }
            ParticipantLoginModel model = new ParticipantLoginModel();
            return View(model);
        }

        [SessionCheckPart]
        //[Authorize(Roles = "User")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string RandomValue, ParticipantLoginModel model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            try
            {
                RandomValue = (Session["RV"] != null && Session["RV"].ToString() != "") ? Session["RV"].ToString() : model.RandomValue;
                if (model != null)
                {
                    DataTable dt = SPManager.SP_LoginForParticipantCheck(model);
                    if (dt.Rows.Count > 0)
                    {
                        Session["SurveyId"] = dt.Rows[0]["SurveyId"].ToString();
                        Session["AssessmentSendLinkPartId_pk"] = dt.Rows[0]["AssessmentSendLinkPartId_pk"].ToString();
                        Session["AssessmentScheduleId_fk"] = dt.Rows[0]["AssessmentScheduleId_fk"].ToString();
                        Session["PartUserId"] = dt.Rows[0]["PartUserId"].ToString();
                        Session["EmailID"] = dt.Rows[0]["EmailID"].ToString();
                        Session["Password"] = dt.Rows[0]["Password"].ToString();
                        Session["RandomValue"] = dt.Rows[0]["RandomValue"].ToString();
                        Session["TrainingCenterId"] = dt.Rows[0]["TrainingCenterId"].ToString();
                        Session["CourseId"] = dt.Rows[0]["CourseId"].ToString();
                        Session["FormId"] = dt.Rows[0]["FormId"].ToString();
                        Session["BatchId"] = dt.Rows[0]["BatchId"].ToString();
                        Session["AssessmentCurStatus"] = dt.Rows[0]["AssessmentCurStatus"].ToString();
                        Session["IsAssessmentExpired"] = dt.Rows[0]["IsAssessmentExpired"].ToString();
                        Session["StartTime"] = dt.Rows[0]["StartTime"].ToString();
                        Session["EndTime"] = dt.Rows[0]["EndTime"].ToString();
                        Session["IsExam"] = dt.Rows[0]["IsExam"].ToString();
                        var assessmentsendid = Guid.Parse(dt.Rows[0]["AssessmentSendLinkPartId_pk"].ToString());
                        var tbl2nd = _db.tbl_AssessmentSendLinkEmail.Find(assessmentsendid);
                        if (!string.IsNullOrWhiteSpace(RandomValue))
                        {
                            model.RandomValue = RandomValue;
                            if (dt.Rows[0]["IsFinalDone"].ToString() == "1")
                            {
                                var assessmentid = Guid.Parse(dt.Rows[0]["AssessmentScheduleId_fk"].ToString());
                                var tbl = _db.tbl_AssessmentSchedule.Find(assessmentid);
                                tbl.AssessmentSchedule = true;
                                tbl.UpdatedOn = DateTime.Now;
                                tbl2nd.AssessmentSchedule = true;
                                tbl2nd.UpdatedOn = DateTime.Now;
                                _db.SaveChanges();
                                return RedirectToAction("AssessmentDone", "ParticipantUser");
                            }
                            else if (dt.Rows[0]["IsFinalDone"].ToString() == "0" && (dt.Rows[0]["IsExam"].ToString() == "1" || dt.Rows[0]["AssessmentCurStatus"].ToString() == "1"))
                            {
                                var assessmentid = Guid.Parse(dt.Rows[0]["AssessmentScheduleId_fk"].ToString());
                                var tbl = _db.tbl_AssessmentSchedule.Find(assessmentid);
                                tbl.AssessmentSchedule = true;
                                tbl.UpdatedOn = DateTime.Now;
                                tbl2nd.AssessmentSchedule = true;
                                tbl2nd.UpdatedOn = DateTime.Now;
                                _db.SaveChanges();

                                if (dt.Rows[0]["IsDraftDone"].ToString() == "1")
                                {
                                    return RedirectToAction("AssessmentDone", "ParticipantUser");
                                }
                                return RedirectToAction("AssessmentExpired", "ParticipantUser");
                            }
                            if (dt.Rows[0]["AssessmentCurStatus"].ToString() == "0" && dt.Rows[0]["IsExam"].ToString() == "0" && dt.Rows[0]["IsFinalDone"].ToString() == "0")
                            {
                                tbl2nd.NoofAttempt += 1;
                                tbl2nd.AttemptDt = DateTime.Now;
                                _db.SaveChanges();
                                return RedirectToAction("Add", "Assessment", new { Id = 0, FId = dt.Rows[0]["CourseId"].ToString(), Rdmkeyno = RandomValue });
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(RandomValue))
                        {
                            if (dt.Rows[0]["IsFinalDone"].ToString() == "1")
                            {
                                return RedirectToAction("AssessmentDone", "ParticipantUser");
                            }
                            else if (dt.Rows[0]["AssessmentCurStatus"].ToString() == "0")
                            {
                                ModelState.AddModelError("", "Please check the mail assessment valid link !!");
                            }
                            else if (dt.Rows[0]["IsExam"].ToString() == "1")
                            {
                                return RedirectToAction("AssessmentExpired", "ParticipantUser");
                            }
                            return View(model);
                            //else if (dt.Rows[0]["AssessmentCurStatus"].ToString() == "1" && dt.Rows[0]["IsDraftDone"].ToString() == "1")
                            // {
                            //     return RedirectToAction("AssessmentDone", "ParticipantUser");
                            // }
                            // else if (dt.Rows[0]["AssessmentCurStatus"].ToString() == "0" && dt.Rows[0]["IsDraftDone"].ToString() == "1")
                            // {
                            //     return RedirectToAction("AssessmentDone", "ParticipantUser");
                            // }

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email ID and Password Invalid !!");
                        return View(model);
                    }
                }
                ModelState.AddModelError("", "Please Enter Email ID and Password !!");
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [SessionCheckPart]
        public ActionResult AssessmentDone()
        {
            CertificateModel model = new CertificateModel();
            string bodydata = string.Empty;
            if (Session["SurveyId"] != null && Session["SurveyId"].ToString() != "")
            {
                DataSet ds = SPManager.GetSP_ScorersSummaryMarks(Session["PartUserId"].ToString(), Convert.ToInt32(Session["FormId"].ToString()), Convert.ToInt32(Session["BatchId"].ToString()));
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        var IsCertf = dt.Rows[0]["IsCertificate"].ToString();
                        model.IsCertificate = IsCertf;
                        model.ScorePercentage = dt.Rows[0]["Percentage"].ToString();
                        if (dt.Rows[0]["IsCertificate"].ToString() == "1")
                        {
                            string bodyTemplate = string.Empty;
                            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/CertificateIssuePart.html")))
                            {
                                bodyTemplate = reader.ReadToEnd();
                            }
                            if (!string.IsNullOrWhiteSpace(bodyTemplate))
                            {
                                dt1 = ds.Tables[1];
                                bodydata = bodyTemplate.Replace("{Name}", dt1.Rows[0]["ReportedNameBy"].ToString())
                           .Replace("{CourseName}", dt1.Rows[0]["CourseName"].ToString())
                           .Replace("{Completion}", "yes")//dt.Rows[0][""].ToString()
                           .Replace("{ConductedBy}", dt1.Rows[0]["TrainerName"].ToString())
                           .Replace("{BatchS}", dt1.Rows[0]["BatchStartDate"].ToString())
                           .Replace("{BatchE}", dt1.Rows[0]["BatchEndDate"].ToString())
                           .Replace("{at}", "")
                           .Replace("{IsIssueDt}", dt1.Rows[0]["CreatedOn"].ToString());
                                model.HrmlData = bodydata;
                            }
                        }
                        return View(model);
                    }
                    return RedirectToAction("Login", "ParticipantUser");
                }
            }
            return RedirectToAction("Login", "ParticipantUser");
        }
        [SessionCheckPart]
        public ActionResult AssessmentExpired()
        {
            return View();
        }
        [SessionCheckPart]
        public ActionResult AssessmentInvalid()
        {
            return View();
        }
    }
}