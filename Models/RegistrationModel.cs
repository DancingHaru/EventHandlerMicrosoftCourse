using System.ComponentModel.DataAnnotations;

namespace EventEase;

public class RegistrationModel
{
    [Required(ErrorMessage = "The Event ID is required.")]
    public int EventId { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
    public string Email { get; set; } = string.Empty;

}