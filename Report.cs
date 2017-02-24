namespace ConsoleApplication
{
    /// <summary>
    /// The Report class is used keep track of a report's file location and the number of issues
    /// detected and resolved in the XML content of the report.
    /// </summary>
    class Report
    {
        /// <summary>
        /// The local file path of the report.
        /// </summary>
        /// <returns>Report path.</returns>
        public string Path { get; private set; }

        /// <summary>
        /// Number of style or formatting issues fixed in the XML content of the report.
        /// </summary>
        /// <returns>Count of issues fixed.</returns>
        public int NumberOfIssuesFixed { get; set; }

        /// <summary>
        /// Initialises a report with a local path and number of issues set to 0.
        /// </summary>
        /// <param name="path">The local file path of the report.</param>
        public Report(string path)
        {
            this.Path = path;
            this.NumberOfIssuesFixed = 0;
        }
    }
}