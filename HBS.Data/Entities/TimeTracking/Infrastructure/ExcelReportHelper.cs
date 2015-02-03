using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.Infrastructure
{
    public class ExcelReportHelper
    {
        public static string GetExcelString(CustomTimeTrack reportObject, string reportName)
        {
            var propertiesToDisplayAsColumns = new Dictionary<string, string>
            {
                {"TotalTimeForTheDay", "Total Time Worked"},
                {"StampDateForDisplay", "Date"},
                {"EmployeeHourlyRate", "Hourly Rate"},
                {"EmployeePayForThePeriod", "Daily Payment"}
            };
            var builder = new StringBuilder();
            AddWorkBookHeader(builder);
            const int sheetCount = 1;


            var sheetName = reportName;//"Sheet(" + sheetCount + ")";
            //sheetName = reportName;

            AddWorkSheetHeader(builder, sheetName);
            AddWorkSheetColumn(builder, new DailyTimeTrack(), propertiesToDisplayAsColumns);
            AddWorkSheetData(builder, reportObject.EmployeeName, reportObject.DailyTimeTracks, propertiesToDisplayAsColumns);
            AddWorksheetFooter(builder);

            AddWorkBookFooter(builder);

            return builder.ToString().Trim();
        }
        public static string GetExcelString(DataSet dataSource)
        {
            var builder = new StringBuilder();
            AddWorkBookHeader(builder);
            const int sheetCount = 1;
            foreach (DataTable table in dataSource.Tables)
            {
                string sheetName = "Sheet(" + sheetCount + ")";
                if (!string.IsNullOrEmpty(table.TableName))
                    sheetName = table.TableName;

                AddWorkSheetHeader(builder, sheetName);
                AddWorkSheetColumn(builder, table);
                AddWorkSheetData(builder, table);
                AddWorksheetFooter(builder);
            }
            AddWorkBookFooter(builder);

            return builder.ToString().Trim();
        }

        private static void AddWorkBookHeader(StringBuilder builder)
        {
            builder.Append("<?xml version='1.0'?>");
            builder.Append(Environment.NewLine + "<?mso-application progid='Excel.Sheet'?> ");
            builder.Append(Environment.NewLine + "<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet' ");
            builder.Append("xmlns:o='urn:schemas-microsoft-com:office:office' ");
            builder.Append("xmlns:x='urn:schemas-microsoft-com:office:excel' ");
            builder.Append("xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' ");
            builder.Append("xmlns:html='http://www.w3.org/TR/REC-html40'> ");
            builder.Append(Environment.NewLine + "<ExcelWorkbook xmlns='urn:schemas-microsoft-com:office:excel'/>");
            builder.Append(Environment.NewLine + "<Styles>");
            builder.Append(Environment.NewLine + "<Style ss:ID='Default' ss:Name='Normal'>");
            builder.Append(Environment.NewLine + "<Font ss:FontName='Calibri' x:Family='Swiss' ss:Size='11' ss:Color='#000000'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='Header'>");
            builder.Append(Environment.NewLine + "<<Alignment ss:Horizontal='Center' ss:Vertical='Center'/>");
            builder.Append(Environment.NewLine + "<Font ss:FontName='Calibri' x:Family='Swiss' ss:Size='11' ss:Color='#000000' ss:Bold='1'/>");
            builder.Append(Environment.NewLine + "<Borders>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "</Borders>");
            builder.Append(Environment.NewLine + "<Interior ss:Color='#C8D7E1' ss:Pattern='Solid'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='Link'>");
            builder.Append(Environment.NewLine + "<Font ss:FontName='Calibri' x:Family='Swiss' ss:Size='11' ss:Color='#0000FF' ss:Underline='Single'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='WrapText'>");
            builder.Append(Environment.NewLine + "<Alignment ss:Vertical='Center' ss:WrapText='1'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='EvenCell'>");
            builder.Append(Environment.NewLine + "<Alignment ss:Vertical='Center' ss:WrapText='1'/>");
            builder.Append(Environment.NewLine + "<Borders>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "</Borders>");
            builder.Append(Environment.NewLine + "<Interior ss:Color='#EDEDED' ss:Pattern='Solid'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='EvenCellNoWrap'>");
            builder.Append(Environment.NewLine + "<Alignment ss:Vertical='Center'/>");
            builder.Append(Environment.NewLine + "<Borders>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "</Borders>");
            builder.Append(Environment.NewLine + "<Interior ss:Color='#EDEDED' ss:Pattern='Solid'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='OddCell'>");
            builder.Append(Environment.NewLine + "<Alignment ss:Vertical='Center' ss:WrapText='1'/>");
            builder.Append(Environment.NewLine + "<Borders>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "</Borders>");
            builder.Append(Environment.NewLine + "<Interior ss:Color='#FFFFFF' ss:Pattern='Solid'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "<Style ss:ID='OddCellNoWrap'>");
            builder.Append(Environment.NewLine + "<Alignment ss:Vertical='Center'/>");
            builder.Append(Environment.NewLine + "<Borders>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>");
            builder.Append(Environment.NewLine + "</Borders>");
            builder.Append(Environment.NewLine + "<Interior ss:Color='#FFFFFF' ss:Pattern='Solid'/>");
            builder.Append(Environment.NewLine + "</Style>");
            builder.Append(Environment.NewLine + "</Styles>");
        }

        private static void AddWorkSheetHeader(StringBuilder builder, string worksheetName)
        {
            builder.Append(Environment.NewLine + string.Format("<Worksheet ss:Name='{0}'>", worksheetName));
            builder.Append(Environment.NewLine + "<Table>");
        }


        private static void AddWorkSheetColumn(StringBuilder builder, DailyTimeTrack dtt, Dictionary<string, string> columnsToDisplay)
        {
            // FOR USER NAME
            builder.Append(Environment.NewLine + "<Column ss:Width='100'/>");
            foreach (var property in dtt.GetType().GetProperties())
            {
                if (columnsToDisplay.ContainsKey(property.Name))
                    builder.Append(Environment.NewLine + "<Column ss:Width='100'/>");
            }

            builder.Append(Environment.NewLine + "<Row>");
            builder.Append(Environment.NewLine + "\t" + "<Cell ss:StyleID='Header'><Data ss:Type='String' > Employee </Data></Cell>");
            foreach (var property in dtt.GetType().GetProperties())
            {
                if (columnsToDisplay.ContainsKey(property.Name))
                    builder.Append(Environment.NewLine + "\t" + "<Cell ss:StyleID='Header'><Data ss:Type='String' >" + columnsToDisplay[property.Name] + "</Data></Cell>");
            }
            builder.Append(Environment.NewLine + "</Row>");
        }

        private static void AddWorkSheetData(StringBuilder builder, string EmployeeName, List<DailyTimeTrack> dttList, Dictionary<string, string> columnsToDisplay)
        {
            int rowIndex = 0;
            foreach (var dtt in dttList)
            {
                ++rowIndex;

                builder.Append(Environment.NewLine + "<Row>");
                int columnIndex = 1;
                builder.Append(Environment.NewLine + GetCell(columnIndex, EmployeeName + "", rowIndex));
                columnIndex++;
                foreach (var property in dtt.GetType().GetProperties())
                {
                    if (columnsToDisplay.ContainsKey(property.Name))
                    {
                        builder.Append(Environment.NewLine + GetCell(columnIndex, property.GetValue(dtt, null) + "", rowIndex));
                        columnIndex++;
                    }
                }

                builder.Append(Environment.NewLine + "</Row>");
            }

        }

        private static void AddWorkSheetColumn(StringBuilder builder, DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                builder.Append(Environment.NewLine + "<Column ss:Width='100'/>");
            }

            builder.Append(Environment.NewLine + "<Row>");
            foreach (DataColumn column in dt.Columns)
            {
                builder.Append(Environment.NewLine + "\t" + "<Cell ss:StyleID='Header'><Data ss:Type='String' >" + column.ColumnName + "</Data></Cell>");
            }
            builder.Append(Environment.NewLine + "</Row>");
        }

        private static void AddWorkSheetData(StringBuilder builder, DataTable dt)
        {
            int rowIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                ++rowIndex;

                builder.Append(Environment.NewLine + "<Row>");
                int columnIndex = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    builder.Append(Environment.NewLine + GetCell(columnIndex, row[column] + "", rowIndex));
                    columnIndex++;
                }

                builder.Append(Environment.NewLine + "</Row>");
            }

        }

        private static string GetCell(int columnIndex, string columnValue, int rowIndex)
        {
            if (rowIndex % 2 == 0)
                return string.Format("\t<Cell ss:StyleID='EvenCell' ss:Index='{0}'><Data ss:Type='String'>{1}</Data></Cell>", columnIndex, EncodeXml(columnValue));

            return string.Format("\t<Cell ss:StyleID='OddCell' ss:Index='{0}'><Data ss:Type='String'>{1}</Data></Cell>", columnIndex, EncodeXml(columnValue));
        }

        private static string EncodeXml(string data)
        {
            if (data == null) return string.Empty;
            data = new System.Text.RegularExpressions.Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)").Replace(data, "&amp;");
            //return data.Replace("<br />", ", ").Replace("<", "&lt;").Replace(">", "&gt;");
            return data.Replace("<br />", "&#10;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        private static void AddWorkBookFooter(StringBuilder builder)
        {
            builder.Append(Environment.NewLine + "</Workbook>");
        }

        public static void AddWorksheetFooter(StringBuilder builder)
        {
            builder.Append(Environment.NewLine + "</Table>");
            builder.Append(Environment.NewLine + "</Worksheet>");
        }
    }
}
