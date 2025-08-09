using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models.CodeFirst
{
    public class ExampleItem
    {
        [Key]
        public int itemId { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        [StringLength(100, ErrorMessage = "Item name must be less than 100 characters.")]
        public string itemName { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Item price must be between 1 and 100000.")]
        [Display(Name = "Item Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal itemPrice { get; set; }
    }
}
