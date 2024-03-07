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
        public ActionResult AddPartictant()
        {
            ParticitantModel model = new ParticitantModel();
            return View();
        }
        // [HttpPost]
        //public ActionResult AddPartictant(ParticitantModel model)
        //{
        //    Hunar_DBEntities db_ = new Hunar_DBEntities();
        //    JsonResponseData response = new JsonResponseData();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var getdt = db_.tbl_Participant.Where(x => x.IsActive == true).ToList();
        //            if (getdt.Any(x => x.PhoneNo == model.PhoneNo.Trim() && model.ID ==Guid.Empty))
        //            {
        //                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Registration.<br /> <span> Case ID : <strong>" + getdt?.FirstOrDefault().CaseID + " </strong>  </span>", Data = null };
        //                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
        //                resResponse1.MaxJsonLength = int.MaxValue;
        //                return resResponse1;
        //            }
        //            var tbl = model.ID != Guid.Empty ? db.tbl_Participant.Find(model.ID) : new tbl_Participant();
        //            if (tbl != null)
        //            {
        //                tbl.tbl_Participant_Child = model.RegID;
        //                tbl. = !(string.IsNullOrWhiteSpace(model.Name)) ? model.Name.Trim() : model.Name;
        //                tbl.MotherName = !(string.IsNullOrWhiteSpace(model.MotherName)) ? model.MotherName.Trim() : model.MotherName;
        //                tbl.FatherName = !(string.IsNullOrWhiteSpace(model.FatherName)) ? model.FatherName.Trim() : model.FatherName;
        //                var sid = Convert.ToInt32(eState.Jharkhand);
        //                tbl.StateId = db.State_Mast.Where(x => x.ID == sid).FirstOrDefault().ID.ToString();
        //                tbl.DistrictId = model.DistrictId;
        //                tbl.BlockId = model.BlockId;
        //                tbl.PanchayatId = model.PanchayatId;
        //                tbl.VillageId = model.VillageId;
        //                tbl.SchoolId = model.SchoolId;
        //                tbl.ClassId = model.ClassId;
        //                //if ("990099" == model.VillageId)
        //                //{
        //                //    tbl.VillageOther = !(string.IsNullOrWhiteSpace(model.VillageOther)) ? model.VillageOther.Trim() : model.VillageOther;
        //                //}
        //                //tbl.DOB = model.DOB;
        //                //tbl.Age = model.Age;
        //                //tbl.MobileNo = !(string.IsNullOrWhiteSpace(model.MobileNo)) ? model.MobileNo.Trim() : model.MobileNo;
        //                //tbl.Sex = "Female";//!(string.IsNullOrWhiteSpace(model.Sex)) ? model.Sex.Trim() : model.Sex;
        //                //tbl.Cast = !(string.IsNullOrWhiteSpace(model.Cast)) ? model.Cast.Trim() : model.Cast;
        //                //tbl.IsActive = true; tbl.IsDeleted = false;

        //                ////tbl.Visited = model.Visited;
        //                ////tbl.IsSkillTraining = model.IsSkillTraining;
        //                ////tbl.IsMarriage = model.IsMarriage;
        //                ////tbl.IsStudy = model.IsStudy;
        //                ////tbl.SocialClass = !(string.IsNullOrWhiteSpace(model.SocialClass)) ? model.SocialClass.Trim() : model.SocialClass;
        //                ////tbl.TillStudied = model.TillStudied;
        //                ////tbl.IsWork = model.IsWork;
        //                ////tbl.Reason = model.Reason;
        //                ////tbl.IsPsychometric = model.IsPsychometric;
        //                ////tbl.PsyYes_Result = !(string.IsNullOrWhiteSpace(model.PsyYes_Result)) ? model.PsyYes_Result.Trim() : model.PsyYes_Result;
        //                ////tbl.Advice = model.Advice;
        //                ////tbl.IsFollowUp = model.IsFollowUp;
        //                ////tbl.FollowUp = !(string.IsNullOrWhiteSpace(model.FollowUp)) ? model.FollowUp.Trim() : model.FollowUp;

        //                //if (model.ID == 0)
        //                //{
        //                //    if (User.Identity.IsAuthenticated)
        //                //    {
        //                //        tbl.CreatedBy = MvcApplication.CUser.Id;
        //                //    }
        //                //    tbl.CreatedDt = DateTime.Now;
        //                //    db.tbl_Registration.Add(tbl);
        //                //}
        //                //else
        //                //{
        //                //    if (User.Identity.IsAuthenticated)
        //                //    {
        //                //        tbl.UpdatedBY = MvcApplication.CUser.Id;
        //                //    }
        //                //    tbl.UpdatedDt = DateTime.Now;
        //                //}
        //                results = db.SaveChanges();
        //            }
        //            if (results > 0)
        //            {
        //                // var taskres = CU_RegLogin(model, tbl.ID);
        //                var case_id = db_.tbl_Registration.Where(x => x.Name == model.Name.Trim() && x.FatherName == model.FatherName.Trim() && x.MotherName == model.MotherName.Trim() && x.DOB == model.DOB)?.FirstOrDefault().CaseID; //if (case_id != null) { }
        //                if (model.ID > 0)
        //                {
        //                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + case_id + " </strong> </span>", Data = null };
        //                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
        //                    resResponse3.MaxJsonLength = int.MaxValue;
        //                    return resResponse3;
        //                }
        //                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully registered! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + case_id + " </strong> </span>", Data = null };
        //                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
        //                resResponse1.MaxJsonLength = int.MaxValue;
        //                return resResponse1;
        //            }
        //        }
        //        else
        //        {
        //            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
        //            var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
        //            resResponse1.MaxJsonLength = int.MaxValue;
        //            return resResponse1;
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
        //        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
        //        resResponse1.MaxJsonLength = int.MaxValue;
        //        return resResponse1;
        //    }


        //    return View();
        //}
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