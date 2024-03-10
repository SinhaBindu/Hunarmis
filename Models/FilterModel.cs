using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class FilterModel
    {
        [Display(Name = "Candidate")]
        public string PartId { get; set; }
        [Display(Name = "Candidate Reg No")]
        public string RegNo { get; set; }
        [Display(Name ="District")]
        public string DistrictId { get; set; }
        [Display(Name = "Batch")]
        public string BatchId { get; set; }
        [Display(Name = "Training Center")]
        public string TrainingCenterID { get; set; }
        [Display(Name = "From Date")]
        public string FromDt { get; set; }
        [Display(Name = "To Date")]
        public string ToDt { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string DOB { get; set; }
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Display(Name = "Role")]
        public string Roles { get; set; }
        public string CutUser { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Search { get; set; }
    }
}