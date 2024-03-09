using Hunarmis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Controllers
{
    public class ParticipantController : Controller
    {
        Hunar_DBEntities db = new Hunar_DBEntities();
        int results = 0; int results2nd = 0;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddParticitant(Guid? Id)
        {
            ParticitantModel model = new ParticitantModel();
            if (Id != Guid.Empty)
            {
                var tbl = db.tbl_Participant.Find(Id);
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
                model.BatchId = tbl.BatchId;
                model.CourseEnrolled = tbl.CourseEnrolled;
                model.TrainingCenterID = tbl.TrainingCenterID;
                model.TrainerName = tbl.TrainerName;
                model.AssessmentScore = tbl.AssessmentScore;    
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddPartictant(ParticitantModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    var getdt = db_.tbl_Participant.Where(x => x.IsActive == true).ToList();
                    if (getdt.Any(x => x.PhoneNo == model.PhoneNo.Trim() && model.ID != Guid.Empty))
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Registration.<br /> <span> Case ID : <strong> </strong>  </span>", Data = null };
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
                        tbl.BatchId = model.BatchId;
                        tbl.AssessmentScore = !(string.IsNullOrWhiteSpace(model.AssessmentScore)) ? model.AssessmentScore.Trim() : model.AssessmentScore;
                        tbl.EduQualificationID = model.EduQualificationID;
                        tbl.CourseEnrolled = model.CourseEnrolled;
                        tbl.TrainingCenterID = model.TrainingCenterID;
                        tbl.TrainerName = !(string.IsNullOrWhiteSpace(model.TrainerName)) ? model.TrainerName.Trim() : model.TrainerName;

                        if (model.ID == Guid.Empty)
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                //tbl.CreatedBy = MvcApplication.CUser.Id;
                            }
                            tbl.CreatedOn = DateTime.Now;
                            db.tbl_Participant.Add(tbl);
                        }
                        else
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                // tbl.UpdatedBy = MvcApplication.CUser.Id;
                            }
                            tbl.UpdatedOn = DateTime.Now;
                        }
                        results = db.SaveChanges();
                    }
                    if (results > 0)
                    {
                        // var taskres = CU_RegLogin(model, tbl.ID);
                        var case_id = db_.tbl_Participant.Where(x => x.PhoneNo == model.PhoneNo.Trim() && x.EmailID == model.EmailID.Trim())?.FirstOrDefault().RegID; //if (case_id != null) { }
                        if (model.ID != Guid.Empty)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + case_id + " </strong> </span>", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully registered! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + case_id + " </strong> </span>", Data = null };
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
        public ActionResult AddPartCalling()
        {
            ParticipantChildModel model = new ParticipantChildModel();
            return View();
        }
        [HttpPost]
        public ActionResult AddPartCalling(Guid? Id)
        {
            ParticipantChildModel model = new ParticipantChildModel();
            if (Id != Guid.Empty)
            {
            }
            return View(model);
        }
        public ActionResult AddPartCalling(ParticipantChildModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    var getdt = db_.tbl_Participant.Where(x => x.IsActive == true && x.ID == model.ParticipantId_fk).ToList();
                    if (db_.tbl_Participant_Calling.Any(x => x.ParticipantId_fk == model.ParticipantId_fk && model.ID == Guid.Empty))
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "This Participant Is Already Exists.<br /> <span> Emp Code : <strong>" + getdt.FirstOrDefault().RegID + " </strong>  </span>", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                    var tbl = model.ID != Guid.Empty ? db.tbl_Participant_Calling.Find(model.ID) : new tbl_Participant_Calling();
                    if (tbl != null)
                    {
                        if (results > 0)
                        {
                            tbl.ParticipantId_fk = model.ParticipantId_fk;
                            tbl.IsCalling = model.IsCalling;
                            tbl.ParticipantId_fk = model.ParticipantId_fk;
                            tbl.ParticipantId_fk = model.ParticipantId_fk;
                            tbl.ParticipantId_fk = model.ParticipantId_fk;
                            if (model.ID == Guid.Empty)
                            {
                                tbl.ID = Guid.NewGuid();
                                tbl.CreatedBy = User.Identity.Name;
                                tbl.CreatedOn = DateTime.Now;
                                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Emp Code : <strong>" + getdt.FirstOrDefault().RegID + " </strong> </span>", Data = null };
                                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse3.MaxJsonLength = int.MaxValue;
                                return resResponse3;
                            }
                            if (model.ID != Guid.Empty)
                            {
                                tbl.UpdatedBy = User.Identity.Name;
                                tbl.UpdatedOn = DateTime.Now;
                                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Emp Code : <strong>" + getdt.FirstOrDefault().RegID + " </strong> </span>", Data = null };
                                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse3.MaxJsonLength = int.MaxValue;
                                return resResponse3;
                            }
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully registered! \r\nPlease Note Your <br /> <span> Emp Code : <strong>" + getdt.FirstOrDefault().RegID + " </strong> </span>", Data = null };
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
    }
}