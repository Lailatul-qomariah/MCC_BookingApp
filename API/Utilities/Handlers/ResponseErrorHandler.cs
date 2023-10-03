using System.Net;

namespace API.Utilities.Handlers;

public class ResponseErrorHandler 
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }



    //proses explore, belum tau cara manggilnya gimana yg masuk ke catch
  /*  public ResponseErrorHandler(TEntity? error)
    {
        Code = StatusCodes.Status500InternalServerError;
        Status = HttpStatusCode.InternalServerError.ToString();
        Message = "Failed to Create data";
        Error = error;
    }*//*



    // untuk delete, getAll dan getbyguid
   *//* public ResponseErrorHandler(string message)
    {
        Code = StatusCodes.Status404NotFound;
        Status = HttpStatusCode.NotFound.ToString();
        Message = message;
    }*/
}
