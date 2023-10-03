using API.Models;

namespace API.DTOs.Educations;

public class CreateEducationDto
// Representasi DTO untuk membuat entitas Education
{
    //properti Education yang akan dibuat
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float Gpa { get; set; }
    public Guid UniversityGuid { get; set; }

    // Operator implisit untuk mengubah objek CreateEducationDto menjadi objek Education
    public static implicit operator Education(CreateEducationDto createEducationDto)
    {
        // Inisiasi objek Education dengan data dari objek CreateEducationDto
        return new Education
        {
            Guid = createEducationDto.Guid,
            Major = createEducationDto.Major,
            Degree = createEducationDto.Degree,
            Gpa = createEducationDto.Gpa,
            UniversityGuid = createEducationDto.UniversityGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
