namespace PdfQuetsche;

internal static class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var argDict = new Dictionary<string, string>();
        
            for (int i = 0; i < args.Length; i += 2)
            {
                if (args[i] != Konstanten.ArgName && args[i] != Konstanten.ArgSource && args[i] != Konstanten.ArgTarget)
                    Console.WriteLine($"'{args[i]}' ist kein bekanntes Commandlet!{Environment.NewLine}Bekannte Commandlets: {Konstanten.ArgName}, {Konstanten.ArgSource}, {Konstanten.ArgTarget}");
                else
                    argDict[args[i]] = args[i + 1];
            }

            using var quetscher = new Quetscher(argDict);
            var erfolgreichGequetscht = quetscher.QuetschIt();

            Console.WriteLine(erfolgreichGequetscht ? 
                                      $"{Environment.NewLine}Quetschung geglückt!{Environment.NewLine}Deine Pdf ist unter '{argDict[Konstanten.ArgTarget]}' zu finden.{Environment.NewLine}" : 
                                      $"{Environment.NewLine}Upsi pupsi, da ist wohl was schiefgegangen!{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}