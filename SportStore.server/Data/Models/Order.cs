namespace SportStore.server.Data.Models;

public class Order
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public IEnumerable<Cart>? Carts { get; set; }
}