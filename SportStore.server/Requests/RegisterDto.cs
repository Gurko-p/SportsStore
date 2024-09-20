using System.ComponentModel.DataAnnotations;

namespace SportStore.server.Requests;

public class RegisterDto : AuthBaseDto
{
    public string? Role { get; set; } = "user";
}