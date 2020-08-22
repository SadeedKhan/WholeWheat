using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WholeWheat.Models.Admin
{
    public class AdminLoginModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string UserPassword { get; set; }
        public string ErrorMessage { get; set; }
    }
}