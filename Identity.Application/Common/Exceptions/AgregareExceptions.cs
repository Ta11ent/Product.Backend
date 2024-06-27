using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Exceptions
{
    public class AgregareExceptions : AggregateException
    {
        public AgregareExceptions(IEnumerable<IdentityError> errors)
            : base(BuilderExceptions(errors)) { }
        public AgregareExceptions(IdentityError error)
            : base(BuilderExceptions(new List<IdentityError> {error})) { }
        private static IEnumerable<Exception> BuilderExceptions(IEnumerable<IdentityError> errors)
        {
            var exceptions = new List<Exception>();
            foreach (var error in errors)
                exceptions.Add(new ArgumentException($"{error.Code}: {error.Description};"));
            return exceptions;
        }
    }
}
