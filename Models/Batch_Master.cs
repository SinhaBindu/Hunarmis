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
    
    public partial class Batch_Master
    {
        public int Id { get; set; }
        public string BatchName { get; set; }
        public Nullable<System.DateTime> BatchStartDate { get; set; }
        public Nullable<System.DateTime> BatchEndDate { get; set; }
        public Nullable<bool> IsAssessmentDone { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
    }
}
