namespace simplecoveragerunner.console
{
    using System.Collections.Generic;

    public static class Constants
    {


        public static List<string> HelpOptiona => new List<string>
        {
            "-?",
            "-help",
        };

        public const string TestProjectPattern = "*Tests";
    }
}