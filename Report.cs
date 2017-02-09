namespace ConsoleApplication
{
    class Report
    {
        public string Path { get; private set; }

        public int NumberOfIssuesFixed { get; set; }

        public Report(string path)
        {
            this.Path = path;
            this.NumberOfIssuesFixed = 0;
        }
    }
}