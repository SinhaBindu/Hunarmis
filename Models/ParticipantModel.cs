using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class ParticipantModel
    {
        public ParticipantModel()
        {
            ID = Guid.Empty;
        }
        public System.Guid ID { get; set; }
        public string RegID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string PhoneNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string EmailID { get; set; }
        public string AadharCardNo { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<DateTime> BatchStartDate { get; set; }
        public Nullable<DateTime> BatchEndDate { get; set; }
        public string AssessmentScore { get; set; }
        public Nullable<int> EduQualificationID { get; set; }
        public string EduQualOther { get; set; }
        public Nullable<int> CourseEnrolledID { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> TrainingAgencyID { get; set; }
        public Nullable<int> TrainingCenterID { get; set; }
        public string TrainerName { get; set; }
    }
}