namespace Application.Contracts.Exceptions;

public class NegativeSumException : Exception
{
    public NegativeSumException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NegativeSumException()
    {
    }

    public NegativeSumException(string message)
        : base(message)
    {
    }
}