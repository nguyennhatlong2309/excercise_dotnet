using Microsoft.AspNetCore.Identity;

namespace examBlazor.Models;

public class ApplicationRole : IdentityRole<int>
{
    public string Description { get; set; } = string.Empty;
}
