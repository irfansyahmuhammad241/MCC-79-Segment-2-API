namespace API.DTOS.Educations
{
    public class NewEducationsDto
    {
        public string Major { get; set; }

        public string Degree { get; set; }

        public double GPA { get; set; }

        public Guid UniversityGuid { get; set; }
    }
}
