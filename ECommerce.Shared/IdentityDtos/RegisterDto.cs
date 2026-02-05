using System.ComponentModel.DataAnnotations;

public record RegisterDto([EmailAddress] string Email, string DisplayName, string Password,string UserName,[Phone]string PhoneNumber);
