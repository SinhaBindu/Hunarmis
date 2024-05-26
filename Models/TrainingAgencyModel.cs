using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hunarmis.Models
{
    public class TrainingAgencyModel
    {
        public TrainingAgencyModel()
        {
            Id = 0;
        }
        public int Id { get; set; }
        [Display(Name = "Training Agency Name")]
        public string TrainingAgencyName { get; set; }
    }
}