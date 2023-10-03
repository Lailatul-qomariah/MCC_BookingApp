using API.Models;

namespace API.DTOs.Universities;

public class UniversityDto
{
    //representasi DTO untuk model atau entitaas University
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    // Operator eksplisit untuk convert objek University ke UniversityDto
    //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create di controller
    public static explicit operator UniversityDto(University university)
    {
        // Inisiasi objek UniversityDto dengan data dari objek University
        return new UniversityDto
        {
            Guid = university.Guid,
            Code = university.Code,
            Name = university.Name
        };
    }

    // Operator implisit untuk convert objek UniversityDto ke University
    //digunakan pada saat menggunakan method Update di controller
    public static implicit operator University(UniversityDto universityDto)
    {
        // Inisiasi objek University dengan data dari objek UniversityDto
        return new University
        {
            Guid = universityDto.Guid,
            Code = universityDto.Code,
            Name = universityDto.Name,
            ModifiedDate = DateTime.Now
        };
    }
}
