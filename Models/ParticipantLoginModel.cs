using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class ParticipantLoginModel
    {
        public ParticipantLoginModel() {
        }
        public System.Guid AssessmentSendLinkPartId_pk { get; set; }
        public Nullable<System.Guid> AssessmentScheduleId_fk { get; set; }
        public Nullable<System.Guid> ParticipantId_fk { get; set; }
        public string RandomValue { get; set; }
        public string AssessmentLink { get; set; }
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
        public Nullable<bool> AssessmentSchedule { get; set; }
        public Nullable<int> IsEmailSend { get; set; }
        public Nullable<int> NoofAttempt { get; set; }
        public Nullable<System.DateTime> AttemptDt { get; set; }
    }
}