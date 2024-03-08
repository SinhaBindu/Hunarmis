using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class ParticitantModel
    {
        public ParticitantModel()
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
        public Nullable<int> CourseEnrolled { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> TrainingAgencyID { get; set; }
        public Nullable<int> TrainingCenterID { get; set; }
        public string TrainerName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }

    public class ParticipantChildModel
    {
        public ParticipantChildModel()
        {
            ID = Guid.Empty;
        }
        public System.Guid ID { get; set; }
        public Nullable<int> IsCalling { get; set; }
        public Nullable<System.Guid> ParticipantId_fk { get; set; }
        public System.Guid RegId { get; set; }
        public string IsAssessmentCert { get; set; }
        public string IsPresent { get; set; }
        public string IsComfortable { get; set; }
        public string EmpCompany { get; set; }
        public string FirstJobTraining { get; set; }
        public Nullable<System.DateTime> DOJ { get; set; }
        public string CurrentEmployer { get; set; }
        public string Designation { get; set; }
        public string SalaryPackage { get; set; }
        public string CurrentlyWorking { get; set; }
        public Nullable<int> WorkingKM { get; set; }
        public string WorkingKMOther { get; set; }
        public string Traininghelp { get; set; }
        public string SalaryWages { get; set; }
        public string ExpectationJobRole { get; set; }
        public string WorkPlaceSafe { get; set; }
        public string IsMSBenefit { get; set; }
        public Nullable<int> MSBenefitId { get; set; }
        public string MSOther { get; set; }
        public string AnyChallenges { get; set; }
        public string AreaSupport { get; set; }
        public Nullable<int> EmployedId { get; set; }
        public string EmployedOther { get; set; }
        public string IsGettingjob { get; set; }
        public string PlacedStatus { get; set; }
    }
}