using Authentication.Service.Utils;

namespace Authentication.Service.Dto;

public class AssignRoleRequestDto
{
    public string Email { get; set; }
    public string Role { get; set; }
}