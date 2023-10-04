using System.Net;

namespace API.Utilities.Handlers;

public class ResponseValidatorHandler
{
    //class dengan beberapa properti untuk menangani error 
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public object Error { get; set; }

    // constructor ini digunakan untuk menangani kasus khusus dengan message custom ketika operasi gagal
    public ResponseValidatorHandler(object error)
    {
        Code = StatusCodes.Status400BadRequest; //status 400
        Status = HttpStatusCode.BadRequest.ToString(); //badrequest
        Message = "Validation Error"; //error mesage nya
        Error = error; //berisi message dari validatornya
    }
}
