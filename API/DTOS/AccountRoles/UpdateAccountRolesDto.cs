using System.ComponentModel.DataAnnotations;

namespace API.DTOS.AccountRoles
{
    public class UpdateAccountRolesDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public Guid AccountGuid { get; set; }
        [Required]
        public Guid RoleGuid { get; set; }
    }
}
