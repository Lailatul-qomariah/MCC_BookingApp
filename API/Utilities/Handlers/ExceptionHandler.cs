using System;

namespace API.Utilities.Handlers;

public class ExceptionHandler : Exception
 //Representasi penanganan exception yang dapat di custom dengan pesan tertentu.
{
    public ExceptionHandler(string message) : base(message) { }
}
