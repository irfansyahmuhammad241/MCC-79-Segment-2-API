using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Rooms
{
    public class UpdateRoomDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
