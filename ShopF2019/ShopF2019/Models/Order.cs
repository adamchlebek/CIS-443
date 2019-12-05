﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopF2019.Models
{

    public class Order
    {
        public int OrderID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        #region
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative.")]
        #endregion
        public int Quantity { get; set; }
        #region
        [Range(0.00, double.MaxValue, ErrorMessage = "Cost must be non-negative.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        #endregion
        public decimal? Cost { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}