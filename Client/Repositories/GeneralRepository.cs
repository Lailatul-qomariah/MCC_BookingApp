using API.Utilities.Handlers; 
using Client.Contracts; 
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repositories;
public class GeneralRepository<Entity, TId> : IRepository<Entity, TId> where Entity : class 
    // GeneralRepository menggunakan generic types Entity dan TId, dan mengimplementasikan interface IRepository.
{
    //Variabel readonly 'request' yang akan digunakan untuk menyimpan URL request.
    protected readonly string request;
    // Variabel readonly 'contextAccessor' yang akan digunakan untuk mengakses data HTTP context.
    private readonly HttpContextAccessor contextAccessor;
    // Variabel 'httpClient' yang akan digunakan untuk berinteraksi dengan API.
    protected HttpClient httpClient; 

    //constructor
    public GeneralRepository(string request) 
    {
        //dependency injection
        this.request = request; 
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7242/api/") // Inisialisasi 'httpClient' dengan alamat API.
        };
        contextAccessor = new HttpContextAccessor(); 
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
    }

    public async Task<ResponseOKHandler<Entity>> Delete(TId id)
    {
        ResponseOKHandler<Entity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
        using (var response = httpClient.DeleteAsync(request + id).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<IEnumerable<Entity>>> Get()
    // Implementasi metode Get yang akan mengambil daftar entitas.
    {
        ResponseOKHandler<IEnumerable<Entity>> entityVM = null;

        // Mengirim request GET ke URL.
        using (var response = await httpClient.GetAsync(request)) 
        {
            // Membaca respon dari request.
            string apiResponse = await response.Content.ReadAsStringAsync();

            // Deserialisasi respon JSON ke dalam objek 'entityVM'.
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<Entity>>>(apiResponse);        
        }

        return entityVM; // Return objek 'entityVM'.
    }

    public async Task<ResponseOKHandler<Entity>> Get(TId id)
    {
        ResponseOKHandler<Entity> entity = null;

        using (var response = await httpClient.GetAsync(request + id))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseOKHandler<Entity>> Post(Entity entity)
    {
        ResponseOKHandler<Entity> entityVM = null; 
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json"); 

        using (var response = httpClient.PostAsync(request, content).Result) 
        {
            string apiResponse = await response.Content.ReadAsStringAsync(); 
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse); 
        }

        return entityVM; // Mengembalikan objek 'entityVM'.
    }

    public async Task<ResponseOKHandler<Entity>> Put(TId guid, Entity entity)
    {
        ResponseOKHandler<Entity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PutAsync(request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
        }
        return entityVM;
    }
}
