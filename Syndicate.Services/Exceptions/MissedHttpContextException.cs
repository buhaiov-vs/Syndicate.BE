namespace Syndicate.Services.Exceptions;

public class MissedHttpContextException : Exception
{
    public MissedHttpContextException() : base("HttpContext is missed")
    {
    }
}
