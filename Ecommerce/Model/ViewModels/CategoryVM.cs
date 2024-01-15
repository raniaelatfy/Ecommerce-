using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Model;
namespace Ecommerce.Model
{
    public class CategoryVM:Category
    {

        [Required(ErrorMessage = "you should insert yor name ")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{4,29}$", ErrorMessage = "the name should contains characters")]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}