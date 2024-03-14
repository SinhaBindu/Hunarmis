using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class ParticipantChildModel
    {
        public ParticipantChildModel()
        {
            ID = Guid.Empty;
        }
        public System.Guid ID { get; set; }
        [Required]
        [Display(Name = DplyPartCalName.QuesMonth)]
        public Nullable<int> QuesMonth { get; set; }
        [Required]
        [Display(Name = DplyPartCalName.QuesYear)]
        public Nullable<int> QuesYear { get; set; }
        public Nullable<int> IsCalling { get; set; }
        public Nullable<System.DateTime> CallingDate { get; set; }
        public Nullable<System.Guid> ParticipantId_fk { get; set; }
        public Nullable<System.Guid> PartId { get; set; }
        [Display(Name = DplyPartCalName.RegID)]
        public string RegID { get; set; }
        [Display(Name = DplyPartCalName.FullName)]
        public string FullName { get; set; }
        [Display(Name = DplyPartCalName.BatchName)]
        public string BatchId { get; set; }
        [Display(Name = DplyPartCalName.BatchName)]
        public string BatchName { get; set; }
        [Display(Name = DplyPartCalName.SBatchDt)]
        public string SBatchDt { get; set; }
        [Display(Name = DplyPartCalName.EBatchDt)]
        public string EBatchDt { get; set; }
        [Display(Name = DplyPartCalName.IsAssessmentCert)]
        public string IsAssessmentCert { get; set; }
        [Display(Name = DplyPartCalName.IsPresent)]
        public string IsPresent { get; set; }
        [Display(Name = DplyPartCalName.IsComfortable)]
        public string IsComfortable { get; set; }
        [Display(Name = DplyPartCalName.EmpCompany)]
        public string EmpCompany { get; set; }
        [Display(Name = DplyPartCalName.FirstJobTraining)]
        public string FirstJobTraining { get; set; }
        [Display(Name = DplyPartCalName.DOJ)]
        public Nullable<System.DateTime> DOJ { get; set; }
        [Display(Name = DplyPartCalName.CurrentEmployer)]
        public string CurrentEmployer { get; set; }
        [Display(Name = DplyPartCalName.Designation)]
        public string Designation { get; set; }
        [Display(Name = DplyPartCalName.SalaryPackage)]
        public string SalaryPackage { get; set; }
        [Display(Name = DplyPartCalName.CurrentlyWorking)]
        public string CurrentlyWorking { get; set; }
        [Display(Name = DplyPartCalName.WorkingKM)]
        public Nullable<int> WorkingKM { get; set; }
        [Display(Name = DplyPartCalName.WorkingKMOther)]
        public string WorkingKMOther { get; set; }
        [Display(Name = DplyPartCalName.Traininghelp)]
        public string Traininghelp { get; set; }
        [Display(Name = DplyPartCalName.SalaryWages)]
        public string SalaryWages { get; set; }
        [Display(Name = DplyPartCalName.ExpectationJobRole)]
        public string ExpectationJobRole { get; set; }
        [Display(Name = DplyPartCalName.WorkPlaceSafe)]
        public string WorkPlaceSafe { get; set; }
        [Display(Name = DplyPartCalName.IsMSBenefit)]
        public string IsMSBenefit { get; set; }
        [Display(Name = DplyPartCalName.MSBenefitId)]
        public Nullable<int> MSBenefitId { get; set; }
        [Display(Name = DplyPartCalName.MSOther)]
        public string MSOther { get; set; }
        [Display(Name = DplyPartCalName.AnyChallenges)]
        public string AnyChallenges { get; set; }
        [Display(Name = DplyPartCalName.AreaSupport)]
        public string AreaSupport { get; set; }
        [Display(Name = DplyPartCalName.EmployedId)]
        public Nullable<int> EmployedId { get; set; }
        [Display(Name = DplyPartCalName.EmployedOther)]
        public string EmployedOther { get; set; }
        [Display(Name = DplyPartCalName.IsGettingjob)]
        public string IsGettingjob { get; set; }
        [Display(Name = DplyPartCalName.PlacedStatus)]
        public string PlacedStatus { get; set; }
    }
    public static class DplyPartCalName
    {
        public const string RegID = "Registration ID";
        public const string FullName = "Name";
        public const string BatchName = "Batch Name";
        public const string SBatchDt = "Batch Start Date";
        public const string EBatchDt = "Batch End Date";
        public const string QuesMonth = "Month";
        public const string QuesYear = "Year";
        public const string IsAssessmentCert = "Did you give the assessment and get certified?";
        public const string IsPresent = "Are you employed at present?";
        public const string IsComfortable = "Are you comfortable to share the details of your employment?";
        public const string EmpCompany = "Which company are you employed in?";
        public const string FirstJobTraining = "In which month did you join the first job after completion of training?";
        public const string DOJ = "What is your date of joining?";
        public const string CurrentEmployer = "How long have you been working with the current employer? ";
        public const string Designation = "What is your designation?";
        public const string SalaryPackage = "Salary Package (CTC in lakhs per annum)";
        public const string CurrentlyWorking = "Which location are you currently working in? ";
        public const string WorkingKM = "How far is the place your are working in located with respect to your nativity?";
        public const string WorkingKMOther = "";
        public const string Traininghelp = "12.9 Did the training help you get this job? ";
        //Section -  General Well-being
        public const string SectionGeneral = "General Well-being";
        public const string SalaryWages = "Do you get paid your salary/wages on time? ";
        public const string ExpectationJobRole = "Is the job role as per your expectation?";
        public const string WorkPlaceSafe = "Is the workplace safe and comfortable? ";
        public const string IsMSBenefit = "Does your job provide you benefits additional to monthly salary?";
        public const string MSBenefitId = "If yes, specify";
        public const string MSOther = "";
        public const string AnyChallenges = "Would you like to seek support with any challenges you may be facing presently? ";
        public const string AreaSupport = "Mention area of support";
        public const string EmployedId = "Why aren't you employed?";
        public const string EmployedOther = "";
        public const string IsGettingjob = "Do you need help getting a job at this juncture";
        //Section - Placement Decision
        public const string SectionPlacement = "Placement Decision";
        public const string PlacedStatus = "Is the candidate placed or not?";
    }
}