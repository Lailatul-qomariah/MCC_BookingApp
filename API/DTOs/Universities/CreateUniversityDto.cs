using API.Models;

namespace API.DTOs.Universities;

public class CreateUniversityDto
// Representasi DTO untuk membuat entitas University
{
    //properti University yang akan dibuat
    public string Code { get; set; }
    public string Name { get; set; }
    
    // Operator implisit untuk mengubah objek CreateUniversityDto menjadi objek University
    public static implicit operator University(CreateUniversityDto createUniversityDto)
    {
        // Inisiasi objek University dengan data dari objek CreateUniversityDto
        return new University
        {
            Code = createUniversityDto.Code,
            Name = createUniversityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
