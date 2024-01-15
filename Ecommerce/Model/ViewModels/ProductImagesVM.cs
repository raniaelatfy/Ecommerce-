using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ecommerce.Model;
namespace Ecommerce.Model.ViewModels
{
    public class ProductImagesVM:ProductImage
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Product Name")]
        public int? ProductFK { get; set; }
    }
}