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
        public string ParticipantId { get; set; }
        [Display(Name = "Candidate")]
        public string ParticipantQuestionId { get; set; }
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
        [Display(Name = "Month")]
        public int MonthId { get; set; }
        [Display(Name = "Month")]
        public string Month { get; set; }
        [Display(Name = "Year")]
        public int YearId { get; set; }
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Search")]
        public string Search { get; set; }
    }
}