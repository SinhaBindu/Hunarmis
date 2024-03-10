using System;
using System.Collections.Generic;
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