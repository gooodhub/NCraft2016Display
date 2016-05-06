using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCraftDisplay.Data.Engine;
using System.Threading;

namespace NCraftdisplay.IntegrationTests
{
    [TestClass]
    public class ConsoleRunnerShould
    {
     
    }

    internal static class PathHelper
    {
        internal static string GetAI(string exeFileName)
        {
            var exePath = Path.Combine(Environment.CurrentDirectory, "..\\..\\TestCases\\", exeFileName);
            if (!File.Exists(exePath))
                throw new FileNotFoundException("Test executable not found, have you run BuildAis.cmd ?");

            return exePath;
        }
    }
}
