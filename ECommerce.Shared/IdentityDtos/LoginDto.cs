using System.ComponentModel.DataAnnotations;

public record LoginDto([EmailAddress] string Email, string Password);
