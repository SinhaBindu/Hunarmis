//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hunarmis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_UserLogin
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> InDate { get; set; }
        public Nullable<System.DateTime> OutDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
