using BlazorDownloadFile;
using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.Task;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.ViewModels.LoggingChangeLogDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CamcoTasks.Data.Services
{
    public class FileManagerService : IFileManagerService
    {
        private IWebHostEnvironment _webHostEnvironment;
        private ILogger<FileManagerService> _logger;
        private IBlazorDownloadFileService _blazorDownloadFileService;

        private const string IMAGEFORMATS = @".jpg|.png|.gif|.jpeg|.bmp|.svg|.jfif|.apng|.ico$";
        private const string FILEFORMATS = @".pdf|.xlsx|.xls|.csv|.xlsb|.pptx|.docx|.graphml$";
        private readonly string _baseFilePath;

        private bool debugging;

        private string _recuringTaskHowtoFileExtention =
            ".docx,.doc,.pdf, .xlsx, .xls, .csv, .xlsb, .pptx, .docx, .jpg,.png,.gif, .jpeg, .bmp,.svg,  .jfif, .apng, .ico, .graphml";


        public FileManagerService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration,
            ILogger<FileManagerService> logger, IBlazorDownloadFileService blazorDownloadFileService)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _blazorDownloadFileService = blazorDownloadFileService;
            _baseFilePath = configuration.GetValue<string>("ServerConfigURL:BaseFilesPath");
        }


        public string RecurringTaskHowtoFileExtention
        {
            get
            {
                return _recuringTaskHowtoFileExtention;
            }
        }

        public bool IsImage(string file)
        {
            if (!string.IsNullOrEmpty(file))
                return Regex.IsMatch(file.ToLower(), IMAGEFORMATS);

            return false;
        }

        public bool IsFile(string file)
        {
            if (!string.IsNullOrEmpty(file))
                return Regex.IsMatch(file.ToLower(), FILEFORMATS);

            return false;
        }

        public bool IsValidSize(double length) => length / 1000000 <= 20;

        public string CreateRecurringTaskDirectory(string TaskIdTarget)
        {
            string path = $"{_baseFilePath}RecurringTasks\\{TaskIdTarget}\\";

            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                return di.FullName;
            }
            else
                return path;
        }
        public string GetBaseTaskDirectory()
        {
            string path = _baseFilePath;

            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                return di.FullName;
            }
            else
                return path;
        }

        public string CreateOneTimeTaskDirectory(string TaskIdTarget)
        {
            string path = $"{_baseFilePath}OneTimeTasks\\{TaskIdTarget}\\";

            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                return di.FullName;
            }
            else
                return path;
        }

        public bool WriteToFile(MemoryStream source, string target)
        {
            try
            {
                if (source == null)
                {
                    throw new FileNotFoundException(string.Format(@"Source file was not found. FileName: {0}", source));
                }

                FileStream filestream = new FileStream(target, FileMode.OpenOrCreate);
                source.WriteTo(filestream);
                filestream.Close();
                source.Close();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "update Report error:", ex);
            }

            return false;
        }

        public string GetFileName(string link)
        {
            return Path.GetFileName(link);
        }

        public bool RunningInDebugMode()
        {
            debugging = false;
            SetIsDebugging();
            return debugging;
        }

        [Conditional("DEBUG")]
        private void SetIsDebugging()
        {
            debugging = true;
        }

        public byte[] CreatePdfForUpdateReport(List<UpdateReportViewModel> updateList, string title, DateTime repoartDate)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
                    Document docx = new Document(pagesize, 15, 15, 15, 15);

                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var subHeaderFont = FontFactory.GetFont("Arial", 18, new BaseColor(System.Drawing.Color.Black));
                    Paragraph subheading = new Paragraph("DATE: " + repoartDate.ToString("MM/dd/yyyy"), subHeaderFont);
                    subheading.Alignment = Element.ALIGN_RIGHT;
                    subheading.SpacingAfter = 0;
                    docx.Add(subheading);

                    var headerFont = FontFactory.GetFont("Arial", 20, new BaseColor(System.Drawing.Color.Black));
                    Paragraph HeaderPara = new Paragraph(title, headerFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 20;
                    docx.Add(HeaderPara);

                    var tblHeaderFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(7)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };
                    float[] widths = new float[] { 4f, 7f, 10f, 10f, 10f, 10f, 46f };
                    ItemsTable.SetWidths(widths);

                    var si = new PdfPCell(new Phrase("SI", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    var taskId = new PdfPCell(new Phrase("Task ID", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    var startTime = new PdfPCell(new Phrase("Start Time", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    var endTime = new PdfPCell(new Phrase("End Time", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    var timeSpent = new PdfPCell(new Phrase("Time Spent", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    var PastAverageTime = new PdfPCell(new Phrase("Past Average Time", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    var description = new PdfPCell(new Phrase("Task Description", tblHeaderFont))
                    {
                        Padding = 5,
                        BorderWidth = 1,
                        BackgroundColor = new BaseColor(235, 233, 230),
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { si, taskId, startTime, endTime, timeSpent, PastAverageTime, description });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    var bodyFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                    int sl = 1;
                    foreach (var item in updateList)
                    {
                        ItemsTable.AddCell(new Phrase((sl++).ToString(), bodyFont));
                        ItemsTable.AddCell(new Phrase(item.TaskId.ToString(), bodyFont));
                        ItemsTable.AddCell(new Phrase(item.StartTime, bodyFont));
                        ItemsTable.AddCell(new Phrase(item.EndTime, bodyFont));
                        ItemsTable.AddCell(new Phrase(item.TimeSpent.ToString(), bodyFont));
                        ItemsTable.AddCell(new Phrase(item.PastAverageTime.ToString(), bodyFont));
                        ItemsTable.AddCell(new Phrase(item.TaskDescription, bodyFont));
                    }
                    docx.Add(ItemsTable);

                    docx.Close();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "update Report error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreatePdfInMemory(List<TasksRecTasksViewModel> TasksList)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 15, 15, 15, 15);
            docx.SetPageSize(PageSize.A4.Rotate());

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFontForDate = FontFactory.GetFont("Arial", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(0, 0, 0)));
                    Paragraph HeaderParaDate = new Paragraph("CURRENT DATE: " + DateTime.Now.ToString("MM-dd-yyyy"),
                        redTextFontForDate);
                    HeaderParaDate.Alignment = Element.ALIGN_RIGHT;
                    HeaderParaDate.SpacingAfter = 0;
                    docx.Add(HeaderParaDate);

                    var redTextFont = FontFactory.GetFont("Arial", 38,
                        new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("RECURRING TASKS REPORT",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 10;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(9)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 20f, 25f, 80f, 25f, 25f, 40f, 40f, 30f, 30f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 10, Font.BOLD,
                        new BaseColor(System.Drawing.Color.White));

                    var IdCell = new PdfPCell(new Phrase("ID", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var TaaskSubjectCell = new PdfPCell(new Phrase("TASK SUBJECT", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var DescriptionCell = new PdfPCell(new Phrase("DESCRIPTION", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                        NoWrap = false
                    };
                    var UpComingDateCell = new PdfPCell(new Phrase("DUE DATE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var InitiatorCell = new PdfPCell(new Phrase("INITIATOR ", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                        NoWrap = false
                    };
                    var PersonCell = new PdfPCell(new Phrase("PERSON RESPONSIBLE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                        NoWrap = false
                    };
                    var LastDateCompletedCell = new PdfPCell(new Phrase("LAST DATE COMPLETED", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var FrequencyCell = new PdfPCell(new Phrase("FREQUENCY", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var LastestValueCell = new PdfPCell(new Phrase("LASTEST VALUE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 8,
                        new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { IdCell, TaaskSubjectCell, DescriptionCell,
                        UpComingDateCell, InitiatorCell, PersonCell, LastDateCompletedCell, FrequencyCell,
                        LastestValueCell });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in TasksList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.Id.ToString(), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.TaskDescriptionSubject, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.UpcomingDate?.ToString("M/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Initiator, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.PersonResponsible, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.DateCompleted?.ToString("M/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Frequency, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.LatestGraphValue?.ToString(), TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreatePdfInMemory error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreatePdfInMemory(List<TasksRecTasksViewModel> TasksList, string activeFilter)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 15, 15, 15, 15);
            docx.SetPageSize(PageSize.A4.Rotate());

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFontForDate = FontFactory.GetFont("Arial", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(0, 0, 0)));
                    Paragraph HeaderParaDate = new Paragraph("CURRENT DATE: " + DateTime.Now.ToString("MM-dd-yyyy"),
                        redTextFontForDate);
                    HeaderParaDate.Alignment = Element.ALIGN_RIGHT;
                    HeaderParaDate.SpacingAfter = 0;
                    docx.Add(HeaderParaDate);

                    var redTextFont = FontFactory.GetFont("Arial", 38, 
                        new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 10;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("RECURRING TASKS REPORT",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 10;
                    docx.Add(ItemPara);

                    var ItemFilterParaSAtyle = FontFactory.GetFont("HELVETICA", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(255, 0, 0)));
                    Paragraph ItemFilterPara = new Paragraph($"ACTIVE FILTERS: {activeFilter}",
                        ItemFilterParaSAtyle);
                    ItemFilterPara.Alignment = Element.ALIGN_CENTER;
                    ItemFilterPara.SpacingAfter = 10;
                    docx.Add(ItemFilterPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(9)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 20f, 25f, 80f, 25f, 25f, 40f, 40f, 30f, 30f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 10, Font.BOLD,
                        new BaseColor(System.Drawing.Color.White));

                    var IdCell = new PdfPCell(new Phrase("ID", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var TaaskSubjectCell = new PdfPCell(new Phrase("TASK SUBJECT", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var DescriptionCell = new PdfPCell(new Phrase("DESCRIPTION", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                        NoWrap = false
                    };
                    var UpComingDateCell = new PdfPCell(new Phrase("DUE DATE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var InitiatorCell = new PdfPCell(new Phrase("INITIATOR ", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                        NoWrap = false
                    };
                    var PersonCell = new PdfPCell(new Phrase("PERSON RESPONSIBLE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                        NoWrap = false
                    };
                    var LastDateCompletedCell = new PdfPCell(new Phrase("LAST DATE COMPLETED", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var FrequencyCell = new PdfPCell(new Phrase("FREQUENCY", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var LastestValueCell = new PdfPCell(new Phrase("LASTEST VALUE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 8,
                        new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { IdCell, TaaskSubjectCell, DescriptionCell, 
                        UpComingDateCell, InitiatorCell, PersonCell, LastDateCompletedCell, FrequencyCell,
                        LastestValueCell });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in TasksList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.Id.ToString(), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.TaskDescriptionSubject, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.UpcomingDate?.ToString("M/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Initiator, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.PersonResponsible, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.DateCompleted?.ToString("M/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Frequency, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.LatestGraphValue?.ToString(), TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreatePdfInMemory error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreateDeactivePdfInMemory(List<TasksRecTasksViewModel> deactiveTasksList)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 15, 15, 15, 15);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFontForDate = FontFactory.GetFont("Arial", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(0, 0, 0)));
                    Paragraph HeaderParaDate = new Paragraph("CURRENT DATE: " + DateTime.Now.ToString("MM-dd-yyyy"),
                        redTextFontForDate);
                    HeaderParaDate.Alignment = Element.ALIGN_RIGHT;
                    HeaderParaDate.SpacingAfter = 0;
                    docx.Add(HeaderParaDate);

                    var redTextFont = FontFactory.GetFont("Arial", 38, new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("Recurring Deactive Tasks", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 10;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(6)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 28f, 25f, 130f, 25f, 25f, 25f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.White));

                    var PersonCell = new PdfPCell(new Phrase("Person Responsible", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var UpComingDateCell = new PdfPCell(new Phrase("Upcoming Date", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var DescriptionCell = new PdfPCell(new Phrase("Description", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var LastDateCompletedCell = new PdfPCell(new Phrase("Last Date Completed", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var FrequencyCell = new PdfPCell(new Phrase("Frequency", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var InitiatorCell = new PdfPCell(new Phrase("Initiator", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { PersonCell, UpComingDateCell, DescriptionCell, LastDateCompletedCell, FrequencyCell, InitiatorCell });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in deactiveTasksList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.PersonResponsible, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.UpcomingDate?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.DateCompleted?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Frequency, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Initiator, TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreatePdfInMemory error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreateCamcoProjectPdfInMemory(List<CamcoProjectsViewModel> projectList)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 15, 15, 15, 15);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFontForDate = FontFactory.GetFont("Arial", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(0, 0, 0)));
                    Paragraph HeaderParaDate = new Paragraph("CURRENT DATE: " + DateTime.Now.ToString("MM-dd-yyyy"),
                        redTextFontForDate);
                    HeaderParaDate.Alignment = Element.ALIGN_RIGHT;
                    HeaderParaDate.SpacingAfter = 0;
                    docx.Add(HeaderParaDate);

                    var redTextFont = FontFactory.GetFont("Arial", 38, new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("Camco Project List", new iTextSharp.text.Font(
                        iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 10;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(7)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 28f, 25f, 25f, 130f, 25f, 25f, 25f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.White));

                    var Champion = new PdfPCell(new Phrase("Champion", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var ProjectCode = new PdfPCell(new Phrase("Project Code", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var Department = new PdfPCell(new Phrase("Department", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var Description = new PdfPCell(new Phrase("Description", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var ProjectTypeName = new PdfPCell(new Phrase("Project TypeName", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var DateCreated = new PdfPCell(new Phrase("DateCreated", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var EnteredBy = new PdfPCell(new Phrase("EnteredBy", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { Champion, ProjectCode, Department, Description,
                        ProjectTypeName, DateCreated, EnteredBy });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in projectList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.Champion, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.ProjectCode, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Department, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.ProjectTypeName, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.DateCreated.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.EnteredBy, TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreatePdfInMemory error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreateEditHistoryPdfInMemory(List<LoggingChangeLogViewModel> projectList)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 15, 15, 15, 15);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFontForDate = FontFactory.GetFont("Arial", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(0, 0, 0)));
                    Paragraph HeaderParaDate = new Paragraph("CURRENT DATE: " + DateTime.Now.ToString("MM-dd-yyyy"),
                        redTextFontForDate);
                    HeaderParaDate.Alignment = Element.ALIGN_RIGHT;
                    HeaderParaDate.SpacingAfter = 0;
                    docx.Add(HeaderParaDate);

                    var redTextFont = FontFactory.GetFont("Arial", 38, new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("Camco Edit History List", new iTextSharp.text.Font(
                        iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 10;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(5)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 25f, 25f, 25f, 25f, 25f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.White));

                    var UpdatedBy = new PdfPCell(new Phrase("CHANGED BY", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var RecordField = new PdfPCell(new Phrase("FIELD CHANGE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var OldValue = new PdfPCell(new Phrase("OLD VALUE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var NewValue = new PdfPCell(new Phrase("NEW VALUE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var UpdateDate = new PdfPCell(new Phrase("DATE CHANGED", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    

                    var TableCellsFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { UpdatedBy, RecordField, OldValue,
                    NewValue, UpdateDate });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in projectList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.UpdatedBy, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.RecordField, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.OldValue, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.NewValue, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.UpdateDate.ToString("M/dd/yyyy"), TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreatePdfInMemory error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreateCamcoProjectPdfInMemory(List<ProjectCosting> projeectCostList)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 15, 15, 15, 15);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFontForDate = FontFactory.GetFont("Arial", 15,
                        new BaseColor(System.Drawing.Color.FromArgb(0, 0, 0)));
                    Paragraph HeaderParaDate = new Paragraph("CURRENT DATE: " + DateTime.Now.ToString("MM-dd-yyyy"),
                        redTextFontForDate);
                    HeaderParaDate.Alignment = Element.ALIGN_RIGHT;
                    HeaderParaDate.SpacingAfter = 0;
                    docx.Add(HeaderParaDate);

                    var redTextFont = FontFactory.GetFont("Arial", 38, new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("Camco Project Costing List", new iTextSharp.text.Font(
                        iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 10;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(7)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 25f, 25f, 130f, 27f, 25f, 25f, 25f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.White));

                    var SoNumber = new PdfPCell(new Phrase("PROJECT CODE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var ProjectTitle = new PdfPCell(new Phrase("TITLE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var Description = new PdfPCell(new Phrase("PROJECT DESCRIPTION", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var Champion = new PdfPCell(new Phrase("CHAMPION", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var Cost = new PdfPCell(new Phrase("TOTAL COST", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var lastActivityDate = new PdfPCell(new Phrase("LAST ACTIVITY DATE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var dateCreated = new PdfPCell(new Phrase("START DATE", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { SoNumber, ProjectTitle, Description, Champion,
                        Cost,lastActivityDate,dateCreated });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var projectCost in projeectCostList)
                    {
                        ItemsTable.AddCell(new Phrase(projectCost.SoNumber, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(projectCost.ProjectTitle, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(projectCost.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(projectCost.Champion, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(projectCost.TotalCost.ToString("C0"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(projectCost.LastActivityDate?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(projectCost.DateCreated?.ToString("MM/dd/yyyy"), TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreatePdfInMemory error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreatePdfRecurringTaskReport(List<TasksRecTasksViewModel> TasksList)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 2, 2, 2, 2);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFont = FontFactory.GetFont("Arial", 38, new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph("Camco Manufacturing Inc.", redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("Recurring Tasks", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 20;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(6)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 28f, 25f, 130f, 25f, 25f, 25f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.White));

                    var PersonCell = new PdfPCell(new Phrase("Person Responsible", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var UpComingDateCell = new PdfPCell(new Phrase("Upcoming Date", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var DescriptionCell = new PdfPCell(new Phrase("Description", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var LastDateCompletedCell = new PdfPCell(new Phrase("Last Date Completed", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var FrequencyCell = new PdfPCell(new Phrase("Frequency", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var InitiatorCell = new PdfPCell(new Phrase("Initiator", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { PersonCell, UpComingDateCell, DescriptionCell, LastDateCompletedCell, FrequencyCell, InitiatorCell });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in TasksList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.PersonResponsible, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.UpcomingDate?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.DateCompleted?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Frequency, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Initiator, TableCellsFont));
                    }

                    docx.Add(ItemsTable);

                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "File Error:", ex);
                }

                return ms.ToArray();
            }
        }

        public byte[] CreatePdfRecurringTaskReport(List<TasksRecTasksViewModel> TasksList, string title, DateTime reportDate)
        {
            iTextSharp.text.Rectangle pagesize = new iTextSharp.text.Rectangle(1080, 1920);
            Document docx = new Document(pagesize, 2, 2, 2, 2);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    PdfWriter.GetInstance(docx, ms);
                    docx.Open();

                    var redTextFont = FontFactory.GetFont("Arial", 38, new BaseColor(System.Drawing.Color.FromArgb(145, 0, 3)));
                    Paragraph HeaderPara = new Paragraph(title, redTextFont);
                    HeaderPara.Alignment = Element.ALIGN_CENTER;
                    HeaderPara.SpacingAfter = 30;
                    docx.Add(HeaderPara);

                    Paragraph ItemPara = new Paragraph("Date: " + reportDate.ToString("MM-dd-yyyy"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
                    ItemPara.Alignment = Element.ALIGN_CENTER;
                    ItemPara.SpacingAfter = 20;
                    docx.Add(ItemPara);

                    PdfPTable ItemsTable;
                    ItemsTable = new PdfPTable(7)
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 98,
                        DefaultCell = { MinimumHeight = 28f },
                        HeaderRows = 1
                    };

                    float[] widths = new float[] { 20f, 28f, 25f, 130f, 25f, 25f, 25f };
                    ItemsTable.SetWidths(widths);
                    var DefaultProductCellFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.White));

                    var IdCell = new PdfPCell(new Phrase("Id", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var PersonCell = new PdfPCell(new Phrase("Person Responsible", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1,
                    };
                    var UpComingDateCell = new PdfPCell(new Phrase("Upcoming Date", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var DescriptionCell = new PdfPCell(new Phrase("Description", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var LastDateCompletedCell = new PdfPCell(new Phrase("Last Date Completed", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var FrequencyCell = new PdfPCell(new Phrase("Frequency", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };
                    var InitiatorCell = new PdfPCell(new Phrase("Initiator", DefaultProductCellFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Black),
                        Padding = 5,
                        BorderWidth = 1
                    };

                    var TableCellsFont = FontFactory.GetFont("Arial", 16, new BaseColor(System.Drawing.Color.Black));

                    PdfPRow row;
                    row = new PdfPRow(new PdfPCell[] { IdCell, PersonCell, UpComingDateCell, DescriptionCell, LastDateCompletedCell, FrequencyCell, InitiatorCell });

                    ItemsTable.DefaultCell.Border = 1;
                    ItemsTable.DefaultCell.Padding = 5;
                    ItemsTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.LightGray);
                    ItemsTable.Rows.Add(row);

                    foreach (var RecTask in TasksList)
                    {
                        ItemsTable.AddCell(new Phrase(RecTask.Id.ToString(), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.PersonResponsible, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.UpcomingDate?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Description, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.DateCompleted?.ToString("MM/dd/yyyy"), TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Frequency, TableCellsFont));
                        ItemsTable.AddCell(new Phrase(RecTask.Initiator, TableCellsFont));
                    }

                    docx.Add(ItemsTable);
                    docx.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "File Error:", ex);
                }

                return ms.ToArray();
            }
        }

        public string ViewRecurringFiles(int fileId, string filePath)
        {
            if (Path.GetExtension(filePath).ToLower() == ".pdf")
            {
                return "/viewrecurringtasks/pdfFileView/" + Convert.ToString(fileId);
            }
            else if (Path.GetExtension(filePath).ToLower() == ".xlsx"
                || Path.GetExtension(filePath).ToLower() == ".xls"
                || Path.GetExtension(filePath).ToLower() == ".xlsb")
            {
                return "/viewrecurringtasks/excelFileView/" + Convert.ToString(fileId);
            }

            return string.Empty;
        }

        public async Task StartDownloadFile(TasksImagesViewModel file)
        {
            if (!File.Exists(file.PictureLink))
            {
                _logger.LogError("Fie Doesn't Exist, Deleted or have been moved");
                return;
            }

            if (!string.IsNullOrEmpty(file.FileName))
            {
                await _blazorDownloadFileService.DownloadFile(file.FileName + Path.GetExtension(file.PictureLink),
                File.ReadAllBytes(file.PictureLink), "application/octet-stream");
            }
            else
            {
                await _blazorDownloadFileService.DownloadFile(Path.GetFileName(file.PictureLink),
                File.ReadAllBytes(file.PictureLink), "application/octet-stream");
            }

        }

        public async Task StartDownloadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _logger.LogError("Fie Doesn't Exist, Deleted or have been moved");
                return;
            }

            await _blazorDownloadFileService.DownloadFile(Path.GetFileName(filePath),
                File.ReadAllBytes(filePath), "application/octet-stream");
        }

        public string FilePathForEmail(TasksImagesViewModel file)
        {
            if (file == null || !File.Exists(file.PictureLink)) return string.Empty;

            string result = string.Empty;

            try
            {
                string destinationPath = CreateRecurringTaskDirectory("FileForEmail");
                destinationPath = destinationPath + Guid.NewGuid().ToString();

                if (Directory.Exists(destinationPath))
                {
                    Directory.Delete(destinationPath);
                }
                else
                {
                    Directory.CreateDirectory(destinationPath);
                }

                if (!string.IsNullOrEmpty(file.PictureLink) && !string.IsNullOrEmpty(file.FileName))
                {
                    destinationPath = destinationPath + "\\" + file.FileName + Path.GetExtension(file.PictureLink);

                    File.Copy(file.PictureLink, destinationPath);

                    result = destinationPath;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Path for Email Error:", ex);
            }

            return result;
        }

        public string ConvertImagetoBase64(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(path);
                    return $"data:image/{Path.GetExtension(path).Replace(".", "").ToLower()};base64, " + Convert.ToBase64String(imageBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public string ConvertImagetoBase64(TasksImagesViewModel file)
        {
            string path = "";

            if (file != null && file.Stream != null)
            {
                byte[] byteImage = file.Stream.ToArray();
                path = $"data:image/png;base64, " + Convert.ToBase64String(byteImage);
            }

            return path;
        }
    }
}

