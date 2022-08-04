﻿namespace Insurance.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal SalesPrice { get; set; }
    public int ProductTypeId { get; set; }
}
