namespace simplecoveragerunner.console
{
    using System.Collections.Generic;
    using System.IO;

    public class Settings
    {
        public string DevelopmentFolder { get; set; } = string.Empty;

        public List<DirectoryInfo> TestFolders { get; set; } = new List<DirectoryInfo>();

        public string ProjectFolderPath { get; set; } = string.Empty;

        public string CoverageFolderPath { get; set; } = string.Empty;

        public string ReportFolderPath { get; set; } = string.Empty;
    }
}
