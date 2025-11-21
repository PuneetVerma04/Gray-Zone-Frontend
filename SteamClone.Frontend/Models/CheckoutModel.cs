using System.ComponentModel.DataAnnotations;

namespace SteamClone.Frontend.Models;

public class CheckoutModel
{
  [Required]
  public string Name { get; set; } = string.Empty;

  [Required, EmailAddress]
  public string Email { get; set; } = string.Empty;

  [Required]
  public string Address { get; set; } = string.Empty;

  [Required, CreditCard]
  public string CardNumber { get; set; } = string.Empty;
}