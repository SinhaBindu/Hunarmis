using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class UserMappedModel
    {
        public UserMappedModel()
        {
            MappedId_pk = Guid.NewGuid();
        }
        public System.Guid MappedId_pk { get; set; }
        [Required]
        [DisplayName("Role")]
        public Nullable<int> RoleId { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserId { get; set; }
        [Required]
        [DisplayName("District")]
        public Nullable<int> DistrictId { get; set; }
        [Required]
        [DisplayName("Training Agency")]
        public Nullable<int> TrainAgencyId { get; set; }
        [Required]
        [DisplayName("Training Center")]
        public string TrainCenterIdMappedMult { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual TrainCenterMPModel TrainCenterMP { get; set; }
    }
    public class TrainCenterMPModel
    {
        public System.Guid TrainCenterMappedId_pk { get; set; }
        public Nullable<int> MappedId_fk { get; set; }
        public Nullable<int> TrainCenterId { get; set; }
    }
}