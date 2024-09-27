using Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Address
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public string Street { get; set; }
    [Phone]
    public string Phone { get; set; }

    public string City { get; set; } = "Rosario";
    public string State { get; set; } = "Santa Fe";
    public string PostalCode { get; set; } = "2000";
    public string Country { get; set; } = "Argentina";

    public Address() { }

    public Address(string street, string phone, int orderId)
    {
        Street = street;
        Phone = phone;
        OrderId = orderId;
    }
}
