namespace Identity.Application.Common.Exceptions
{
    public class NotFoundExceptions : Exception
    {
        public NotFoundExceptions(string name, object key)
            : base($"There is no record for the '{name}' object with the '{key}' key") { }
    }
}
