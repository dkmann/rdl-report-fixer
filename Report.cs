namespace ConsoleApplication
{
    class Report
    {
        public string Path { get; set; }

        public int NumberOfIssuesFixed { get; set; }

        public Report(string path)
        {
            this.Path = path;
        }
    }
}