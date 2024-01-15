namespace Ecommerce.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        public int ID { get; set; }

        public int ProductFK { get; set; }

        [Required]
        [StringLength(2000)]
        public string Image { get; set; }

        public bool IsMine { get; set; }

        public virtual Product Product { get; set; }
    }
}
