using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Model
{
    public class SubCategoryVM:SubCategory
    {
        [Required(ErrorMessage = "you should insert yor name ")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{4,29}$", ErrorMessage = "the name should contains characters")]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Display(Name="Category Name")]
        public int? CategoryFK { get; set; }
    }
}