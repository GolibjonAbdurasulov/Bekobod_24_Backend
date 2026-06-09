namespace Core.Exceptions;

public class ParameterInvalidException : Exception
{
    public ParameterInvalidException(string message) : base(message) { }
}
