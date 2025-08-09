using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models;

public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Item name is required.")]
    [StringLength(150, ErrorMessage = "Item name cannot exceed 150 characters.")]
    [Display(Name = "Item Name")]
    public string ItemName { get; set; } = null!;

    [Required(ErrorMessage = "Item description is required.")]
    [StringLength(500, ErrorMessage = "Item description cannot exceed 500 characters.")]
    [Display(Name = "Item Description")]
    public string ItemDescription { get; set; } = null!;

    [Required(ErrorMessage = "Item cost is required.")]
    [Column(TypeName = "decimal(10, 2)")]
    [DataType(DataType.Currency)]
    [Display(Name = "Item Cost")]
    public decimal ItemCost { get; set; }
    
    [Required(ErrorMessage = "Item image is required.")]
    [StringLength(255, ErrorMessage = "Item image path cannot exceed 255 characters.")]
    [Display(Name = "Item Image")]
    public string ItemImage { get; set; } = null!;

    public int? CategoryId { get; set; }

    public virtual ItemCategory Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
