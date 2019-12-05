using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopF2019.Models
{

    public class Product
    {
        public int ProductID { get; set; }
        #region
        [StringLength(50, MinimumLength = 3)]
        #endregion
        public string Name { get; set; }
        #region
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative")]
        #endregion
        public int Quantity { get; set; }
        #region
        [Range(0.00, double.MaxValue, ErrorMessage = "UnitPrice must be non-negative")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        #endregion
        public decimal UnitPrice { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}