using System.ComponentModel.DataAnnotations;
using EShopMobile.ViewModels;

namespace EShopMobile.Models
{
    /// <summary>
    /// An instance of a product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The id of the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The description of the product
        /// </summary>
        [Required]
        [Display(Name = "Product Description")]
        public string Name { get; set; }

        /// <summary>
        /// The category of the product
        /// </summary>
        [Required(ErrorMessage = "Category is Required")]
        public Category Category { get; set; }

        /// <summary>
        /// The color of the product
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The price of the product
        /// </summary>
        [Required]
        public float Price { get; set; }

        /// <summary>
        /// The remaining quantity of the product
        /// </summary>
        public int? Quantity { get; set; }
    }
}