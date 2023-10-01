using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

//controller generic dan inheritance dengan controller base dan hanya menerima tipe generic berupa class
public class GenericAllController<T> : ControllerBase where T : class
{
    private readonly IAllRepository<T> _repositoryT; //akses interface IAllrepository

    //constructor or dependency injection dengan parameter yang merupakan instance dari IAllRepository<T> 
    public GenericAllController(IAllRepository<T> repositoryT) 
    {
        _repositoryT = repositoryT;
    }

    [HttpGet] //menangani permintaan GET ke endpoint 
    //IActionResult = menangani permintaan http dan memberikan respon http
    public IActionResult GetAll() //method action yang akan dipanggil ketika ada permintaan getAll
    {
        var result = _repositoryT.GetAll(); 
        if (!result.Any()) //cek apakah var result kosong atau tidak
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")] //menangani permintaan GET by Id ke endpoint 
    public IActionResult GetByGuid(Guid guid) 
    {
        var result = _repositoryT.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(T T)
    {
        var result = _repositoryT.Create(T);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] T values) // [FromBody] digunakan untk menerima data yang akan digunakan untuk memperbarui model 
    {
        var existingRepository = _repositoryT.GetByGuid(guid); // get data dari contract/model yg akan diupdate by id

        if (existingRepository == null) //cek apakah data yang akan diupdate null 
        {
            return NotFound($"{_repositoryT}not found");
        }

        if (!ModelState.IsValid) //cek apakah data sesuai validasi 
        {
            return BadRequest(ModelState);
        }


        var entityType = typeof(T); //identifikasi tipe entitas atau model yang akan diupdate
        var properties = entityType.GetProperties(); //get semua properti dari type sebelumnya

        foreach (var property in properties) //looping untuk mendapatkan semua properti
        {
            if (property.Name != "Guid") // cek apakah propertinya bernama Guid? hal ini dilakukan agar Guid tidak dapat diupdate
            {
                var newValue = property.GetValue(values); //get new value dari inputan user
                property.SetValue(existingRepository, newValue); //update value properti yg ada dengan dengan value yang baru atau inputan user
            }

        }

        var updatedRepository = _repositoryT.Update(existingRepository); //manggil method update untuk update data

        if (updatedRepository == null) //cek apakah yg diupdate null
        {
            return BadRequest("Failed to update university");
        }

        return Ok(updatedRepository);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingRepository = _repositoryT.GetByGuid(guid);

        if (existingRepository == null)
        {
            return NotFound($"{_repositoryT} not found");
        }

        var deletedRepository = _repositoryT.Delete(existingRepository);

        if (deletedRepository == null)
        {
            return BadRequest($"Failed to delete {_repositoryT}");
        }

        return Ok(deletedRepository);
    }


}

