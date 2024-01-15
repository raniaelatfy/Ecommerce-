using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Model.ViewModels
{
    public class ProductVM:Product
    {
        [Required(ErrorMessage = "you should insert yor name ")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{4,29}$", ErrorMessage = "the name should contains characters")]
        public string Name { get; set; }
       
        [Display(Name="Category Name")]
        public int? CategoryFK { get; set; }
        [Display(Name ="Brand Name")]
        public int? BrandFK { get; set; }
        [StringLength(500)]

        [Display(Name = "Description")]
        public string ShortDescription { get; set; }
        [AllowHtml] 
        public string Description { get; set; }

    }
}