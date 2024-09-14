namespace Application.Contracts.Exceptions;

public class WrongUserNameException : Exception
{
    public WrongUserNameException(string message)
        : base(message)
    {
    }

    public WrongUserNameException()
    {
    }

    public WrongUserNameException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}