using System;

namespace Shared.Models.User.Dtos.GetDto;

public class UserPublicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UserPrivateDto : UserPublicDto
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
