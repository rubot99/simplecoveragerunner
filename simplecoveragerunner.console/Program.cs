namespace simplecoveragerunner.console
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var settings = ConsoleExtensions.WriteWelcomeScreen();

                List<string> arguments = ConsoleExtensions.ArgumentsToList(args);

                bool stopProcessing = false;

                if (arguments.Count == 0 || ConsoleExtensions.CheckArgument(arguments, OptionsConstants.HelpOption, stopProcessing))
                {
                    stopProcessing = ConsoleExtensions.WriteHelpScreen();
                }

                if (ConsoleExtensions.CheckArgument(arguments, OptionsConstants.ProjectFolderOption, stopProcessing))
                {
                    settings.ProjectFolderPath =
                        ConsoleExtensions.GetArgumentValue(arguments, OptionsConstants.ProjectFolderOption);

                    if (Directory.Exists(settings.ProjectFolderPath))
                    {
                        settings.TestFolders =
                            ConsoleExtensions.GetDirectoryList(settings.ProjectFolderPath,
                                Constants.TestProjectPattern);
                    }
                }
                else
                {
                    // error
                }

                if (ConsoleExtensions.CheckArgument(arguments, OptionsConstants.CoverageFolderOption, stopProcessing))
                {
                    settings.CoverageFolderPath =
                        $"{ConsoleExtensions.GetArgumentValue(arguments, OptionsConstants.CoverageFolderOption)}\\coverage";

                    ConsoleExtensions.PurgeAndCreate(settings.CoverageFolderPath);

                    if (ConsoleExtensions.CheckArgument(arguments, OptionsConstants.ReportFolderOption, stopProcessing))
                    {
                        settings.ReportFolderPath =
                            $"{ConsoleExtensions.GetArgumentValue(arguments, OptionsConstants.ReportFolderOption)}\\report";

                        ConsoleExtensions.PurgeAndCreate(settings.ReportFolderPath);

                        ConsoleExtensions.CoverageProcessing(settings, true);
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleExtensions.WriteErrorScreen(e);
            }
        }
    }
}