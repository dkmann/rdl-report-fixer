using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    class ReportFixer
    {
        private string _report;

        private int _numberOfIssuesFixed;

        public void LoadReport(string report)
        {
            this._report = report;
            this._numberOfIssuesFixed = 0;
        }

        public void FixUnits()
        {
            // Create a temporary file path where we can write modify lines.
            string temporaryFile = Path.Combine(Path.GetDirectoryName(this._report),
                "report-temp.rdl");

            // Open a stream for the temporary file.
            using (var temporaryFileStream = new StreamWriter(File.OpenWrite(temporaryFile)))
            {
                using (var sourceFile = File.OpenText(this._report))
                {
                    string line;

                    // Read lines while the file has them.
                    while ((line = sourceFile.ReadLine()) != null)
                    {
                        // Do the pattern replacement.
                        ConvertCmToMm(ref line);
                        RoundDecimals(ref line);
                        ReplaceReportUnitType(ref line);

                        // Write the modified line to the new file.
                        temporaryFileStream.WriteLine(line);
                    }
                }
            }
            // Replace current with temporary.
            File.Delete(this._report);
            File.Move(temporaryFile, this._report);
        }

        private void ConvertCmToMm(ref string line)
        {
            var pattern = @"(\d+(\.\d+)?)cm";

            line = Regex.Replace(line, pattern,
                match =>
                {
                    this._numberOfIssuesFixed++;
                    var number = double.Parse(match.Groups[1].Value);
                    return (number * 10).ToString() + "mm";
                });
        }

        private void RoundDecimals(ref string line)
        {
            var pattern = @"(\d+\.\d+)mm";

            line = Regex.Replace(line, pattern,
                match =>
                {
                    this._numberOfIssuesFixed++;
                    var number = double.Parse(match.Groups[1].Value);
                    number = Math.Round(number, 0, MidpointRounding.AwayFromZero);
                    return (number).ToString() + "mm";
                });
        }

        private void ReplaceReportUnitType(ref string line)
        {
            var pattern = "<rd:ReportUnitType>Cm</rd:ReportUnitType>";

            line = Regex.Replace(line, pattern,
                match =>
                {
                    this._numberOfIssuesFixed++;
                    return "<rd:ReportUnitType>Mm</rd:ReportUnitType>";
                });
        }
    }
}