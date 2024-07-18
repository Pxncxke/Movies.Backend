namespace Movies.Api.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string name, object key) : base($"{name} ({key}) Forbidden")
    {

    }
}