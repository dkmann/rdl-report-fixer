using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Report Fixer! Enter the path of a report to begin tidying.");
            var fixer = new ReportFixer();
            var reportPath = Console.ReadLine();
            fixer.LoadReport(reportPath);
            Console.WriteLine("Processing report...");
            fixer.FixUnits();
            Console.WriteLine($"Processed report at: {reportPath}");

            // if (numberOfIssuesFixed == 0)
            // {
            //     Console.WriteLine("No issues were found.");
            // }
            // else if (numberOfIssuesFixed == 1) {
            //     Console.WriteLine("1 issue was fixed.");
            // }
            // else
            // {
            //     Console.WriteLine($"{numberOfIssuesFixed} issues were fixed.");
            // }
        }
    }
}
