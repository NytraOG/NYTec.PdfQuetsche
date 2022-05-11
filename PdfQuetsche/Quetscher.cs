using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfQuetsche.Exceptions;

namespace PdfQuetsche;

public class Quetscher : IDisposable
{
    private readonly Dictionary<string, string> args;
    private readonly string                     folderName = $"Quetschgut_{DateTime.Now:dd-MM-yyyy}";

    public Quetscher(Dictionary<string, string> args) => this.args = args;

    public void Dispose() { }

    public bool QuetschIt()
    {
        ValidateDirectories();

        var fileName = new Random().Next(0, 6666).ToString();

        if (args.ContainsKey(Konstanten.ArgName))
            fileName = args[Konstanten.ArgName];

        var baseDirectory       = Path.Combine(args[Konstanten.ArgTarget], "Pdf-Quetsche");
        var baseDirectoryExists = Directory.Exists(baseDirectory);

        if (!baseDirectoryExists)
            Directory.CreateDirectory(baseDirectory);

        var dailyDirectory       = Path.Combine(baseDirectory, folderName);
        var dailyDirectoryExists = Directory.Exists(dailyDirectory);

        if (!dailyDirectoryExists)
            Directory.CreateDirectory(dailyDirectory);

        var sourceFiles = Directory.GetFiles(args[Konstanten.ArgSource])
                                   .Where(f => f.Contains("jpg") || f.Contains("jpeg") || f.Contains("png"));

        var       document = new Document(PageSize.A4.Rotate());
        var       tar      = Path.Combine(dailyDirectory, fileName + ".pdf");
        using var stream   = new FileStream(tar, FileMode.Create, FileAccess.Write, FileShare.None);

        PdfWriter.GetInstance(document, stream);

        document.Open();

        foreach (var file in sourceFiles)
        {
            using var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            imageStream.Seek(0, SeekOrigin.Begin);

            var image = Image.GetInstance(imageStream);

            image.SetAbsolutePosition(0, 0);
            image.ScaleAbsoluteHeight(document.PageSize.Height);
            image.ScaleAbsoluteWidth(document.PageSize.Width);

            document.NewPage();
            document.Add(image);
        }

        document.Close();

        return true;
    }

    private void ValidateDirectories()
    {
        var sourceExists = args.ContainsKey(Konstanten.ArgSource) && Directory.Exists(args[Konstanten.ArgSource]);

        if (!sourceExists)
            throw new DirectoryUnreachableException(args[Konstanten.ArgSource]);

        var targetExists = args.ContainsKey(Konstanten.ArgTarget) && Directory.Exists(args[Konstanten.ArgTarget]);

        if (!targetExists)
            throw new DirectoryUnreachableException(args[Konstanten.ArgTarget]);
    }
}