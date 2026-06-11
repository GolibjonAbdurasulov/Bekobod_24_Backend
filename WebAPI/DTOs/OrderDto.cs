namespace WebAPI.DTOs;

public class OrderDto
{
    public long Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public List<OrderItemDto> Items { get; set; }
}