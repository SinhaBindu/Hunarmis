using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hunarmis.Models
{
    public class TrainingCentreModel
    {
        public TrainingCentreModel()
        {
            Id = 0;
        }
        public int Id { get; set;}
        [Required]
        [Display(Name = DplyTrainingCentreName.DistrictID)]
        public Nullable<int> DistrictID { get; set; }
        [Required]
        [Display(Name = DplyTrainingCentreName.TrainingAgencyID)]
        public Nullable<int> TrainingAgencyID { get; set; }
        [Required]
        [Display(Name = DplyTrainingCentreName.TrainingCenter)]
        public string TrainingCenter { get; set; }
        [Required]
        [Display(Name = DplyTrainingCentreName.Location)]
        public string Location { get; set; }
    }
    public static class DplyTrainingCentreName
    {
        public const string DistrictID = "District";
        public const string TrainingAgencyID = "Training Agency";
        public const string TrainingCenter = "Training Center";
        public const string Location = "Location";
    }
}