using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class AssessmentScheduleModel
    {
        public AssessmentScheduleModel()
        {
            AssessmentScheduleId_pk = Guid.Empty;
            //TrainingCenterId_fk = 0;
        }
        public System.Guid AssessmentScheduleId_pk { get; set; }
        [DisplayName("Date")]
        public Nullable<System.DateTime> Date { get; set; }
        [DisplayName("Start Time")]
        public Nullable<System.TimeSpan> StartTime { get; set; }
        [DisplayName("Start Time")]
        public string StrStartTime { get; set; }
        [DisplayName("End Time")]
        public Nullable<System.TimeSpan> EndTime { get; set; }
        [DisplayName("End Time")]
        public string StrEndTime { get; set; }
        [DisplayName("Batch")]
        public Nullable<int> BatchId_fk { get; set; }
        public Nullable<int> TrainingCenterId_fk { get; set; }
        public Nullable<bool> AssessmentSchedule { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}