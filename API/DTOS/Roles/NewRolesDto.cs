using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Roles
{
    public class NewRolesDto
    {
        [Required]
        public string Name { get; set; }
    }
}
