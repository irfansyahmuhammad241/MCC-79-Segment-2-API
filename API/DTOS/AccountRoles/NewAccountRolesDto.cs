using System.ComponentModel.DataAnnotations;

namespace API.DTOS.AccountRoles
{
    public class NewAccountRolesDto
    {
        [Required]
        public Guid AccountGuid { get; set; }
        [Required]
        public Guid RoleGuid { get; set; }
    }
}
