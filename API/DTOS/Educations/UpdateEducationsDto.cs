using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Educations
{
    public class UpdateEducationsDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        [Range(0, 4, ErrorMessage = "GPA Must Be Between 0 and 4")]
        public double GPA { get; set; }
        [Required]
        public Guid UniversityGuid { get; set; }
    }
}
