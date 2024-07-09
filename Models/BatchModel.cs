using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class BatchModel
    {
        public BatchModel()
        {
            Id = 0;
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Batch Name")]
        public string BatchName { get; set; }
        [Required]
        [Display(Name = "Batch Start Date")]
        public Nullable<System.DateTime> BatchStartDate { get; set; }
        [Required]
        [Display(Name = "Batch End Date")]
        public Nullable<System.DateTime> BatchEndDate { get; set; }
        [Required]
        [Display(Name = "Trainer")]
        public Nullable<Guid> TrainerId { get; set; }
        [Required]
        [Display(Name = "Course")]
        public Nullable<int> CourseId { get; set; }
        [Required]
        [Display(Name = "Training Center")]
        public Nullable<int> TrainingCenterId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
    }
}