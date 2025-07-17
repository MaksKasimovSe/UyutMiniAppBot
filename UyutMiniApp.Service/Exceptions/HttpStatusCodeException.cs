namespace UyutMiniApp.Service.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public HttpStatusCodeException(int code, string message) : base(message)
        {
            StatusCode = code;
        }
    }
}
