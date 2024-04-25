using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class EducationalModel
    {
        public EducationalModel()
        {
            Id = 0;
        }
        public int Id { get; set; }
        [Display(Name = "Qualification")]
        public string QualificationName { get; set; }
    }
}