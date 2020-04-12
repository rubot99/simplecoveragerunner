namespace simplecoveragerunner.console
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public static class ConsoleExtensions
    {
        public static bool CoverageProcessing(Settings settings, bool openReport = true)
        {
            foreach (var settingsTestFolder in settings.TestFolders)
            {
                string testRunnerArgs =
                    $"test {settingsTestFolder.FullName}\\{settingsTestFolder.Name}.csproj /p:CollectCoverage=true /p:CoverletOutput={settings.CoverageFolderPath}\\{settingsTestFolder.Name}.xml /p:CoverletOutputFormat=opencover";

                var testRunnerProcess = System.Diagnostics.Process.Start(new ProcessStartInfo("dotnet", testRunnerArgs));
                testRunnerProcess.WaitForExit();

                string reportRunnerArgs =
                    $" -reports:{settings.CoverageFolderPath}\\{settingsTestFolder.Name}.xml -targetdir:{settings.ReportFolderPath}\\{settingsTestFolder.Name}";

                var reportRunnerProcess = System.Diagnostics.Process.Start("reportgenerator", reportRunnerArgs);
                reportRunnerProcess.WaitForExit();

                if (openReport)
                {
                    if (Directory.Exists("c:\\Program Files (x86)\\Google\\Chrome\\Application"))
                    {
                        var reportViewerProcess =
                            System.Diagnostics.Process.Start(
                                $"c:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe",
                                $"{settings.ReportFolderPath}\\{settingsTestFolder.Name}\\index.htm");
                    }
                }
            }

            return true;
        }

        public static void WriteErrorScreen(Exception ex)
        {
            Console.WriteLine("An error occured:");
            Console.WriteLine(ex.Message);

            Console.ReadLine();
        }

        public static bool WriteHelpScreen()
        {
            Console.WriteLine("Help");

            Console.WriteLine("/p - solution folder");
            Console.WriteLine("/r r- report folder");
            Console.WriteLine("/c - coverage folder");
            Console.WriteLine("Help - /?");

            return true;
        }

        public static Settings WriteWelcomeScreen()
        {
            Console.WriteLine("Simple Coverage Tool");

            return new Settings();
        }

        public static List<string> ArgumentsToList(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                return args.ToList();
            }

            return new List<string>();
        }

        public static bool CheckArgument(List<string> args, string key, bool stopProcessing)
        {
            return stopProcessing == false && args.Exists(x => x.Contains(key));
        }

        public static string GetArgumentValue(List<string> args, string key)
        {
            return args.FirstOrDefault(
                x => x.Contains(key))?
                .Replace(key, String.Empty);
        }

        public static List<DirectoryInfo> GetDirectoryList(string rootfolder, string searchPattern)
        {
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();

            DirectoryInfo projDir = new DirectoryInfo(rootfolder);
            var testDirs =
                projDir.EnumerateDirectories(searchPattern, SearchOption.AllDirectories);

            foreach (var testDir in testDirs)
            {
                dirs.Add(testDir);
            }

            return dirs;
        }

        public static void PurgeAndCreate(string rootFolderpath)
        {
            if (Directory.Exists(rootFolderpath))
            {
                Directory.Delete(rootFolderpath, true);
            }

            Directory.CreateDirectory(rootFolderpath);
        }
    }
}