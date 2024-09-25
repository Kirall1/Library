namespace Library.Business.Exceptions
{
    public class ClientClosedRequest : Exception
    {
        public ClientClosedRequest(string message) : base(message) { }
    }
}
