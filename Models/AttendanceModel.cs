using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class AttendanceModel
    {
        public AttendanceModel()
        {
            AttendanceId_pk = Guid.Empty;
        }
        public System.Guid AttendanceId_pk { get; set; }
        [DisplayName("Batch")]
        [Required]
        public Nullable<int> BatchId { get; set; }
        [DisplayName("Course")]
        [Required]
        public string CourseIds { get; set; }
        [DisplayName("Latitude")]
        [Required]
        public string Lat { get; set; }
        [DisplayName("Longitude")]
        [Required]
        public string Long { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        public string AttendPartIds { get; set; }
        [DisplayName("Session")]
        [Required]
        public string TopicIds { get; set; }
        [DisplayName("Date")]
        public Nullable<System.DateTime> StartDate { get; set; }
        //[DisplayName("End Date")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [DisplayName("Start Time")]
        public Nullable<System.TimeSpan> StartTime { get; set; }
        [DisplayName("Start Time")]
        [Required]
        public string StrStartTime { get; set; }
        [DisplayName("End Time")]
        public Nullable<System.TimeSpan> EndTime { get; set; }
        [DisplayName("End Time")]
        [Required]
        public string StrEndTime { get; set; }
        public virtual AttendPartModel AttendPartlist { get; set; }
        public virtual AttendPartTopicModel Topiclist { get; set; }
        [DisplayName("Other")]
        public string TopicOther { get; set; }
    }
    public class AttendPartModel
    {
        public AttendPartModel()
        {
            AttendancePartId_pk = Guid.Empty;
        }
        public System.Guid AttendancePartId_pk { get; set; }
        public Nullable<System.Guid> AttendanceId_fk { get; set; }
        public Nullable<System.Guid> ParticipantId_fk { get; set; }
    }
    public class AttendPartTopicModel
    {
        public AttendPartTopicModel()
        {
            AttendanceTopicId_pk = Guid.Empty;
        }
        public System.Guid AttendanceTopicId_pk { get; set; }
        public Nullable<System.Guid> AttendanceId_fk { get; set; }
        public Nullable<int> TopicId { get; set; }
       
    }
}