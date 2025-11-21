namespace SteamClone.Frontend.Models;

public class CartItemDto
{
    public int GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}