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
    
    public partial class tbl_LogBatchTrainer
    {
        public int ID { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<System.Guid> TrainerId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> TrainingCenterId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
