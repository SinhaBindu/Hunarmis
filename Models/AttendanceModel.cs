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
        public Nullable<int> BatchId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Address { get; set; }
        public string AttendPartIds { get; set; }
        public string TopicIds { get; set; }
        [DisplayName("Date")]
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        [DisplayName("Started Time")]
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public string StrStartTime { get; set; }
        [DisplayName("Started Time")]
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public string StrEndTime { get; set; }
        public virtual AttendPartModel AttendPartlist { get; set; }
        public virtual AttendPartTopicModel Topiclist { get; set; }
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