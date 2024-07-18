namespace Movies.Api.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string name, object key) : base($"{name} ({key}) Unauthorized")
    {

    }
}
