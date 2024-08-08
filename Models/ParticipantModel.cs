using Hunarmis.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Models
{
    public class ParticipantModel
    {
        public ParticipantModel()
        {
            ID = Guid.Empty;
        }
        public System.Guid ID { get; set; }
        /// <summary>
        /// [Required]
        /// </summary>
        [Display(Name = DplyPartName.RegID)]
        public string RegID { get; set; }
        [Required]
        [Display(Name = DplyPartName.FirstName)]
        public string FirstName { get; set; }
        //[Required]
        [Display(Name = DplyPartName.MiddleName)]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = DplyPartName.LastName)]
        public string LastName { get; set; }
        [Display(Name = DplyPartName.FullName)]
        public string FullName { get; set; }
        [Required]
        [Display(Name = DplyPartName.Gender)]
        public string Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        [Required]
        [Display(Name = DplyPartName.Age)]
        public string Age { get; set; }
        [Required]
        [Display(Name = DplyPartName.PhoneNo)]
        public string PhoneNo { get; set; }
        //[Required]
        [Display(Name = DplyPartName.AlternatePhoneNo)]
        public string AlternatePhoneNo { get; set; }
        [Required]
        [Display(Name = DplyPartName.EmailID)]
        public string EmailID { get; set; }
        [Required]
        [Display(Name = DplyPartName.AadharCardNo)]
        public string AadharCardNo { get; set; }
        [Required]
        [Display(Name = DplyPartName.BatchId)]
        public Nullable<int> BatchId { get; set; }
        //[Required]
        [Display(Name = "Batch Start Date")]
        public Nullable<DateTime> BatchStartDate { get; set; }
        [Display(Name = "Batch End Date")]
        public Nullable<DateTime> BatchEndDate { get; set; }
        [Required]
        [Display(Name = DplyPartName.AssessmentScore)]
        public string AssessmentScore { get; set; }
        [Required]
        [Display(Name = DplyPartName.EduQualificationID)]
        public Nullable<int> EduQualificationID { get; set; }
        [Display(Name = DplyPartName.EduQualOther)]
        public string EduQualOther { get; set; }
        [Required]
        [Display(Name = DplyPartName.CourseEnrolledID)]
        public Nullable<int> CourseEnrolledID { get; set; }
        [Required]
        [Display(Name = DplyPartName.StateID)]
        public Nullable<int> StateID { get; set; }
        [Required]
        [Display(Name = DplyPartName.DistrictID)]
        public Nullable<int> DistrictID { get; set; }
        [Required]
        [Display(Name = DplyPartName.TrainingAgencyID)]
        public Nullable<int> TrainingAgencyID { get; set; }
        [Required]
        [Display(Name = DplyPartName.TrainingCenterID)]
        public Nullable<int> TrainingCenterID { get; set; }
        [Required]
        [Display(Name = DplyPartName.TrainerName)]
        public Guid? TrainerId { get; set; }
        public string TrainerName { get; set; }
       
        [Display(Name = DplyPartName.IsPlaced)]
        public bool IsPlaced { get; set; }
        [Required]
        [Display(Name = DplyPartName.IsPlaced)]
        public string Is_Placed { get; set; }
        [Display(Name = DplyPartName.IsOffered)]
        public Nullable<bool> IsOffered { get; set; }
        [Required]
        [Display(Name = DplyPartName.IsOffered)]
        public string Is_Offered { get; set; }
    }
    public static class DplyPartName
    {
        public const string RegID = "Registration ID";
        public const string FullName = "Name";
        public const string FirstName = "First Name";
        public const string MiddleName = "Middle Name";
        public const string LastName = "Last Name";
        public const string Gender = "Gender";
        public const string Age = "Age";
        public const string PhoneNo = "Phone No";
        public const string AlternatePhoneNo = "Alternate Phone No";
        public const string EmailID = "Email ID";
        public const string AadharCardNo = "Aadhar Card No";
        public const string BatchId = "Batch Name";
        public const string AssessmentScore = "Assessment Score";
        public const string EduQualificationID = "Educational Qualification";
        public const string EduQualOther = "Educational Qualification Other";
        public const string CourseEnrolledID = "Course Enrolled";
        public const string StateID = "State";
        public const string DistrictID = "District";
        public const string TrainingAgencyID = "Training Agency";
        public const string TrainingCenterID = "Training Center";
        public const string TrainerName = "Trainer Name";
        public const string IsOffered = "Offered";
        public const string IsPlaced = "Placed";
    }
}