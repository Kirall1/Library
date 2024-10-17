namespace Library.BusinessAccess.Exceptions
{
    public class ClientClosedRequestException : Exception
    {
        public ClientClosedRequestException(string message) : base(message) { }
    }
}
