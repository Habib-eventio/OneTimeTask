using CamcoTasks.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CamcoTasks.Infrastructure.Common.Email;

public static class EmailDefaults
{

    public static readonly string[] ExceptionEmails = Array.Empty<string>();

    public const string ExceptionEmailSubject = "Exception Email - Human Resource - CAMCO";

    private static string StartTextBasedEmail()
    {
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.Append("<!DOCTYPE html>\n");
        htmlBuilder.Append("<html lang=\"en\">\n");
        htmlBuilder.Append("<head>\n");
        htmlBuilder.Append("\t<meta charset=\"UTF-8\" />\n");
        htmlBuilder.Append("\t<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\n");
        htmlBuilder.Append("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\n");
        htmlBuilder.Append("\t<title>Document</title>\n");
        htmlBuilder.Append("<style>");
        htmlBuilder.Append("thead th {padding: 0px 10px;}");
        htmlBuilder.Append("tbody td {padding: 5px 10px;font-size: 12px;border-bottom: #f5f5f5 1px solid;}");
        htmlBuilder.Append("</style>");
        htmlBuilder.Append("</head>\n");
        htmlBuilder.Append(
            "<body style=\"padding: 0px; margin: 0px; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #262626 ;\">\n");
        htmlBuilder.Append("\t<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\n");
        htmlBuilder.Append("\t\t<tr bgcolor=\"#ffffff\">\n");
        htmlBuilder.Append("\t\t\t<td style=\"padding: 10px\">\n");
        htmlBuilder.Append("\t\t\t\t<p>TO WHOM MAY IT CONCERN</p>\n");
        return htmlBuilder.ToString();
    }

    private static string EndTextBasedEmail(string appName)
    {
        StringBuilder htmlBuilder = new StringBuilder();

        htmlBuilder.Append("\t\t\t</td>\n");
        htmlBuilder.Append("\t\t</tr>\n");
        htmlBuilder.Append("\t\t<tr bgcolor=\"#ffffff\">\n");
        htmlBuilder.Append("\t\t\t<td align=\"left\" style=\"padding: 10px\" valign=\"center\">\n");
        htmlBuilder.Append(
            "\t\t\t\t<img src=\"https://hr.camcomfginc.com/Templates/EmailTemplates/Images/camco-logo.png\" alt=\"CAMCO MANUFACTURING\" />\n");
        htmlBuilder.Append($"\t\t\t\t<p>©{DateTime.Now.Year} COPYRIGHT: CAMCO {appName.ToUpper()}</p>\n");
        htmlBuilder.Append("\t\t\t</td>\n");
        htmlBuilder.Append("\t\t</tr>\n");
        htmlBuilder.Append("\t</table>\n");
        htmlBuilder.Append("</body>\n");
        htmlBuilder.Append("</html>\n");

        return htmlBuilder.ToString();
    }

    private static string TableStartMain(string tableWidth)
    {
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.Append("<tr bgcolor='#ffffff'>");
        htmlBuilder.Append("<td style='padding: 10px; width='100%'>");
        htmlBuilder.Append("<div style='max-width: 100vw; overflow-x: auto'>");
        htmlBuilder.Append($"<table cellspacing='0' cellpadding='0' width='{tableWidth}'>");

        return htmlBuilder.ToString();
    }

    private static string TableEndMain()
    {
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.Append("</table>");
        htmlBuilder.Append("</div>");
        htmlBuilder.Append("</td>");
        htmlBuilder.Append("</tr>");

        return htmlBuilder.ToString();
    }

    public static StringBuilder EmailTableHead(string colSpanCount, string title, List<ColumnForTableModel> columns,
        StringBuilder additionalHeaders, List<ColumnForTableModel> stackedUpRowColumns = null)
    {
        StringBuilder tableHeadStart = new StringBuilder();
        tableHeadStart.Append("<thead>");
        tableHeadStart.Append("<tr bgcolor='#262626'>");
        tableHeadStart.Append(
            $"<th colspan='{colSpanCount}' height='45' style='color: #ffffff; font-size: 14px' valign='middle'>{title}</th>");
        tableHeadStart.Append("</tr>");
        tableHeadStart.Append(additionalHeaders);
        if (stackedUpRowColumns is not null)
        {
            tableHeadStart.Append("<tr bgcolor='#262626' style='font-size: 12px; color: #ffffff'>");
            foreach (var column in stackedUpRowColumns)
            {
                tableHeadStart.Append(
                    $"<th style='{column.CellStyles}' width='{column.Width}' height='{column.CellHeight}' colspan='{column.ColSpanCount}' valign='middle' align='{column.CellDataAlignment}'>{column.Name}</th>");
            }

            tableHeadStart.Append("</tr>");
        }

        tableHeadStart.Append("<tr bgcolor='#343a40' style='font-size: 12px; color: #ffffff'>");
        foreach (var column in columns)
        {
            tableHeadStart.Append(
                $"<th style='{column.CellStyles}' width='{column.Width}' height='{column.CellHeight}' colspan='{column.ColSpanCount}' valign='middle' align='{column.CellDataAlignment}'>{column.Name}</th>");
        }

        tableHeadStart.Append("</tr>");
        tableHeadStart.Append("</thead>");
        return tableHeadStart;
    }

    public static StringBuilder EmailTableBody(List<MainRowForTableModel> rows)
    {
        StringBuilder tableBody = new StringBuilder();
        tableBody.Append("<tbody>");

        int alternativeGrayColor = 1;
        foreach (var mainRow in rows)
        {
            string grayLines = string.Empty;
            if (alternativeGrayColor % 2 == 1)
            {
                grayLines = " style='background-color:#D3D3D3;'";
            }

            tableBody.Append($"<tr{grayLines}>");

            foreach (var rowData in mainRow.RowsData)
            {
                tableBody.Append(
                    $"<td style='padding: 5px 10px; font-size: 12px; border-bottom: #f5f5f5 1px solid;text-align:{rowData.CellDataAlignment}' bgcolor='{rowData.CellBackGroundColor}'>{rowData.Data}</td>");
            }

            tableBody.Append("</tr>");

            alternativeGrayColor++;
        }

        tableBody.Append("</tbody>");

        return tableBody;
    }

    public static string GenerateEmailTemplate(string appName, string innerTemplate)
    {
        return StartTextBasedEmail() + innerTemplate + EndTextBasedEmail(appName);
    }

    public static string GenerateTableTemplate(string tableWidth, string innerTable)
    {
        return TableStartMain(tableWidth) + innerTable +
               TableEndMain();
    }
}