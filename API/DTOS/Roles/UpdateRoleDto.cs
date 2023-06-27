using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Roles
{
    public class UpdateRoleDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
