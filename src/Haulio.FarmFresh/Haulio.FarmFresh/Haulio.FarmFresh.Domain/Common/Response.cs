namespace Haulio.FarmFresh.Domain.Common
{
    public class Response
    {
        public Response(object data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
