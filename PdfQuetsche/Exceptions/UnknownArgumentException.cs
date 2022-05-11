namespace PdfQuetsche.Exceptions;

public class UnknownArgumentException : Exception
{
    public UnknownArgumentException(string argumentKey) : base($"Argument '{argumentKey}' not part of known commandlets.") { }
}