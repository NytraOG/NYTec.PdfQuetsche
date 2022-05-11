namespace PdfQuetsche.Exceptions;

public class DirectoryUnreachableException : Exception
{
    public DirectoryUnreachableException(string directory) : base($"Directory [{directory}] is not reachable.") { }
}