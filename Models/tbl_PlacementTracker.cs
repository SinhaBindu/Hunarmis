//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hunarmis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_PlacementTracker
    {
        public System.Guid PlacementTrackerId_pk { get; set; }
        public Nullable<System.Guid> ParticipantId_fk { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<int> NoofFamilyMembers { get; set; }
        public Nullable<decimal> AnnualHouseholdincome { get; set; }
        public Nullable<int> EmployeeTypeId { get; set; }
        public Nullable<int> IndustryId { get; set; }
        public Nullable<bool> PreTrainingStatus { get; set; }
        public Nullable<bool> TargetGroup { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string PinCode { get; set; }
        public Nullable<System.DateTime> DateofOffer { get; set; }
        public Nullable<System.DateTime> DateofJoining { get; set; }
        public Nullable<System.DateTime> DOJStartDate { get; set; }
        public Nullable<System.DateTime> DOJEndDate { get; set; }
        public string UploadOfferLetterPath { get; set; }
        public string UploadAppointmentLetterPath { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
    
        public virtual tbl_Participant tbl_Participant { get; set; }
    }
}