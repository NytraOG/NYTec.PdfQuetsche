using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PdfQuetsche.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void QuetschIt_ExistierenderBasePfad_NeuesDirectoryAngelegt()
    {
        var baseTargetPath = @"X:\Spielplatz";
        var sourcePath     = @"X:\Spielplatz\Pdf-Quetsche\TestSource";

        var args = new Dictionary<string, string>
        {
            { Konstanten.ArgTarget, baseTargetPath },
            { Konstanten.ArgSource, sourcePath }
        };

        var quetsche = new Quetscher(args);

        quetsche.QuetschIt();
    }
}