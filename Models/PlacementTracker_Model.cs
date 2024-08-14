using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class PlacementTracker_Model
    {
        public PlacementTracker_Model()
        {
            PlacementTrackerId_pk = Guid.Empty;
            modelbasicpart = new BasicDetailsForPartModel();
            FilterModel=new FilterModel();
        }
        public System.Guid PlacementTrackerId_pk { get; set; }
        public Nullable<System.Guid> ParticipantId_fk { get; set; }
        [Required]
        [Display(Name = "Employee Type")]
        public Nullable<int> EmployeeTypeId { get; set; }
        [Required]
        [Display(Name = "Industry")]
        public Nullable<int> IndustryId { get; set; }
       
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required]
        [Display(Name = "Salary (Monthly INR)")]
        public Nullable<decimal> Salary { get; set; }
        [Required]
        [Display(Name = "State")]
        public Nullable<int> StateId { get; set; }
        [Required]
        [Display(Name = "District")]
        public Nullable<int> DistrictId { get; set; }
        [Required]
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }
        [Required]
        [Display(Name = "Date of Offer")]
        public Nullable<System.DateTime> DateofOffer { get; set; }
        [Required]
        [Display(Name = "Date of Joining")]
        public Nullable<System.DateTime> DateofJoining { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public Nullable<System.DateTime> DOJStartDate { get; set; }
        //[Required]
        [Display(Name = "End Date")]
        public Nullable<System.DateTime> DOJEndDate { get; set; }
        [Required]
        [Display(Name = "Upload Offer Letter")]
        public HttpPostedFileBase UploadOfferLetter { get; set; }
        public string UploadOfferLetterPath { get; set; }
        //[Required]
        [Display(Name = "Upload Appointment Letter")]
        public HttpPostedFileBase UploadAppointmentLetter { get; set; }
        public string UploadAppointmentLetterPath { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }

        public virtual tbl_Participant tbl_Participant { get; set; }

        public BasicDetailsForPartModel modelbasicpart { get; set; }
        public FilterModel FilterModel { get; set; }
    }
}