using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class CoursesModel
    {
        public CoursesModel()
        {
            Id = 0;
        }
        public int Id { get; set; }
        [Display(Name = "Course")]
        public string CourseName { get; set; }
    }
}