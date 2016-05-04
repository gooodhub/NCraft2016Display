using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCraftDisplay.Data.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCraftdisplay.IntegrationTests
{
    [TestClass]
    public class ConsoleRunnerShould
    {
        [TestMethod]
        public void Echo_to_stdin_program()
        {
            var runner = new ConsoleRunner(PathHelper.GetAI("MirrorInputAI.exe"));
            runner.Start();
            runner.SendMsg("O");
            runner.SendMsg("EXIT");
            runner.WaitForExit(2000);

            Assert.AreEqual("OO", runner.GetOutPut());
        }

        [TestMethod]
        public void Read_output_of_program()
        {
            var runner = new ConsoleRunner(PathHelper.GetAI("BasicAI.exe"));
            runner.Start();
            runner.WaitForExit(2000);

            Assert.IsTrue(runner.GetOutPut().Length > 50);
        }
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
