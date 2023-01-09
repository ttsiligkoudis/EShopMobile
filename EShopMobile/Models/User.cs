using System;
using System.ComponentModel.DataAnnotations;
using EShopMobile.ViewModels;

namespace EShopMobile.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Display(Name = "Registration Date")]
        public DateTime RegDate { get; set; }
        [Display(Name = "Last Login Date")]
        public DateTime? LoginDate { get; set; }
    }
}