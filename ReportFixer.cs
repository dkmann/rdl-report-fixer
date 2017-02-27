using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    /// <summary>
    /// The ReportFixer class loads a RDL report and scans for style issues in the report that can
    /// lead to bad practices such as alignment issues, and writes the changes back into the report
    /// file. The report must have a valid path and must be XML based.
    /// </summary>
    class ReportFixer
    {
        // An XML based report to be modified.
        private Report _report;

        /// <summary>
        /// Loads an RDL report into the ReportFixer in preparation for applying style fixes.
        /// </summary>
        /// <param name="report">A XML based report.</param>
        public void LoadReport(string report)
        {
            this._report = new Report(report);
        }

        /// <summary>
        /// Reads an RDL file and looks for style issues in the report:
        /// * Centimetre units that need to be converted to decimals for easier precision;
        /// * Decimal units in report that cause alignment issues;
        /// * The default unit type of centimetres that will place wrong units.
        ///
        /// report objects. Fixes all of these issues and writes the corrections to the report file.
        /// Makes a copy of the report for writing to until the entire file stream has been read.
        /// Original file is replaced by the modified file when StreamWriter has completed.
        /// </summary>
        public void FixUnits()
        {
            // Create a temporary file path where we can write modify lines.
            string temporaryFile = Path.Combine(Path.GetDirectoryName(this._report.Path),
                "report-temp.rdl");

            // Open a stream for the temporary file.
            using (var temporaryFileStream = new StreamWriter(File.OpenWrite(temporaryFile)))
            {
                using (var sourceFile = File.OpenText(this._report.Path))
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

            // Replace the current file with temporary one.
            File.Delete(this._report.Path);
            File.Move(temporaryFile, this._report.Path);
        }

        /// <summary>
        /// Converts centimetre units to millimetre units. Takes a line of a report definition and
        /// looks for patterns which are centimetre units and converts them to millimetres. For
        /// example: 1.0cm -> 10mm.
        /// </summary>
        /// <param name="line">An XML line of a report definition file.</param>
        private void ConvertCmToMm(ref string line)
        {
            var pattern = @"(\d+(\.\d+)?)cm";

            // Look for a centimetre pattern in the XML line.
            line = Regex.Replace(line, pattern,
                match =>
                {
                    this._report.NumberOfIssuesFixed++;

                    // Parse the capture group for the numeric part of the pattern.
                    var number = double.Parse(match.Groups[1].Value);

                    return (number * 10).ToString() + "mm";
                });
        }

        /// <summary>
        /// Rounds decimal millimetre units to the nearest integer. Takes a line of a report
        /// definition and looks for patterns which are millimetre units and rounds them. For
        /// example: 1.34mm -> 1mm. We do not round centimetres because we lose more accuracy on the
        /// original value of the number.
        /// </summary>
        /// <param name="line">An XML line of a report definition file.</param>
        private void RoundDecimals(ref string line)
        {
            var pattern = @"(\d+\.\d+)mm";

            // Look for a decimal millimetre pattern.
            line = Regex.Replace(line, pattern,
                match =>
                {
                    this._report.NumberOfIssuesFixed++;

                    // Parse capture group for numeric part of the pattern.
                    var number = double.Parse(match.Groups[1].Value);

                    number = Math.Round(number, 0, MidpointRounding.AwayFromZero);
                    return (number).ToString() + "mm";
                });
        }

        /// <summary>
        /// Replaces the ReportUnitType attribute with millimetres from the report default of
        /// centimetres.
        /// </summary>
        /// <param name="line">An XML line of a report definition file.</param>
        private void ReplaceReportUnitType(ref string line)
        {
            var pattern = "<rd:ReportUnitType>Cm</rd:ReportUnitType>";

            // Look for ReportUnitType attribute.
            line = Regex.Replace(line, pattern,
                match =>
                {
                    this._report.NumberOfIssuesFixed++;
                    return "<rd:ReportUnitType>Mm</rd:ReportUnitType>";
                });
        }
    }
}