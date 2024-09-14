namespace Application.Contracts.Exceptions;

public class MinusBalanceException : Exception
{
    public MinusBalanceException(string message)
        : base(message)
    {
    }

    public MinusBalanceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public MinusBalanceException()
    {
    }
}