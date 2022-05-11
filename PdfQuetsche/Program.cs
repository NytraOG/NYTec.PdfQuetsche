using System.Runtime.CompilerServices;

namespace PdfQuetsche;

internal static class Program
{
    private static void Main(string[] args)
    {
        try
        {
            if(!args.Any())
            {
                Console.WriteLine($"{Environment.NewLine}Suitable commands are:{Environment.NewLine}" +
                                  $"{Environment.NewLine}\t-source\t| Source folder containing the images to merge into a pdf" +
                                  $"{Environment.NewLine}\t-target\t| Target folder for the created pdf" +
                                  $"{Environment.NewLine}\t-name\t| Alternative name for the pdf");
                
                return;
            }

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