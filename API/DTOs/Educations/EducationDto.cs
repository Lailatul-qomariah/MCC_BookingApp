using API.Models;

namespace API.DTOs.Educations;

public class EducationDto
//representasi DTO untuk model atau entitaas Education
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float Gpa { get; set; }
    public Guid UniversityGuid { get; set; }

    // Operator eksplisit untuk convert objek Education ke objek EducationDto
    //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create
    public static explicit operator EducationDto(Education education)
    {
        // Inisiasi objek EducationDto dengan data dari objek Education
        return new EducationDto
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            Gpa = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }
    // Operator implisit untuk convert objek EducationDto ke Education
    //digunakan pada saat menggunakan method Update
    public static implicit operator Education(EducationDto educationDto)
    {
        // Inisiasi objek Education dengan data dari objek EducationDto
        return new Education
        {
            Guid = educationDto.Guid,
            Major = educationDto.Major,
            Degree = educationDto.Degree,
            Gpa = educationDto.Gpa,
            UniversityGuid = educationDto.UniversityGuid,
            ModifiedDate = DateTime.Now
        };
    }
}
