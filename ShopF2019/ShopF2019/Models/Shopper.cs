
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopF2019.Models
{
     public class Shopper
    {
        public int ShopperID { get; set; }
        #region
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        #endregion
        public string LastName { get; set; }
        #region
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        #endregion
        public string FirstName { get; set; }
        #region
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        #endregion
        public string Email { get; set; }

        #region
        [Display(Name = "Full Name")]
        #endregion
        public ICollection<Cart> Carts { get; set; }
    }
}