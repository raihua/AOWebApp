using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [NotMapped]
    [DisplayName("Full Name")]
    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = null!;

    public string MainPhoneNumber { get; set; } = null!;

    public string? SecondaryPhoneNumber { get; set; }

    [NotMapped]
    [DisplayName("Contact Number")]
    public string ContactNumber => (!string.IsNullOrWhiteSpace(MainPhoneNumber) ? MainPhoneNumber : (!string.IsNullOrWhiteSpace(SecondaryPhoneNumber) ? SecondaryPhoneNumber : "No contact number provided"));

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
