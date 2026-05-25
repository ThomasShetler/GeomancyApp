namespace GeomancyWebUI.Client.Services
{
    public static class WorkspaceEntryQuery
    {
        public const string SetupParam = "setup";
        public const string SetupMothers = "mothers";

        public static string BuildMothersSetupPath(string basePath) =>
            $"{basePath}?{SetupParam}={SetupMothers}";
    }
}
