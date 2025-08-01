using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.XlsIO;
using System.Text.RegularExpressions;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Files.ExcelFile
{
    public partial class ExcelFileView : IDisposable
    {
        protected ExcelEngine ExcelEngine;

        public IWorkbook Workbook;

        public IWorksheet Worksheet;

        private FileStream _fileStream;

        protected bool IsLoading { get; set; } = true;
        protected bool IsDoing { get; set; } = false;
        protected bool IsAdd { get; set; } = false;
        protected bool IsUpdate { get; set; } = false;
        protected bool IsActiveExcelFileinputDataComponent { get; set; } = false;

        protected TasksImagesViewModel SelectFile { get; set; } = new TasksImagesViewModel();

        protected List<IRange> SelectedData { get; set; } = new List<IRange>();


        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ViewFileComponent",
            DateCreated = DateTime.Now
        };

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private ILogger<ExcelFileView> _Logger { get; set; }
        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IFileManagerService FileManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Parameter]
        public string FileId { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFileData();

                await PageLoadTimeCalculation();

                await Task.Run(() => IsLoading = false);

                StateHasChanged();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadFileData()
        {
            try
            {
                SelectFile = await taskService.GetUpdateTaskImageAsync(Convert.ToInt32(FileId));
                await Task.Run(() => GetDataFromFiles());
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View file error", ex);
            }

        }

        protected void GetDataFromFiles()
        {
            if (SelectFile == null || string.IsNullOrEmpty(SelectFile.PictureLink))
            {
                return;
            }

            try
            {
                string ext = Path.GetExtension(SelectFile.PictureLink);

                if (!string.IsNullOrEmpty(ext) && (ext.ToLower() == ".xlsx" || ext.ToLower() == ".xls"
                    || ext.ToLower() == ".xlsb"))
                {
                    ExcelEngine = new ExcelEngine();
                    IApplication application = ExcelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Xlsx;

                    _fileStream = new FileStream(SelectFile.PictureLink, FileMode.Open);

                    Workbook = application.Workbooks.Open(_fileStream);

                    Worksheet = Workbook.Worksheets[0];
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Excell file:", ex);
                _toastService.ShowSuccess("File Is Busy In Another Process. Please Try Again After Some Time.");
            }
        }

        private string GetColumnName(string columnName)
        {
            return Regex.Replace(columnName, @"\d", "");
        }

        private string GetCellValue(IRange column)
        {
            Worksheet.EnableSheetCalculations();

            string value = Worksheet[column.AddressLocal].CalculatedValue;

            Worksheet.DisableSheetCalculations();

            return value;
        }

        protected void CreateNewExcelRow()
        {
            if (Worksheet != null)
            {
                int rowCount = Worksheet.Rows.Count() + 1;
                Worksheet.InsertRow(rowCount);
            }
        }

        protected async Task SaveExcelFile()
        {
            try
            {
                await Task.Run(() => IsDoing = true);

                using (MemoryStream stream = new MemoryStream())
                {
                    Workbook.SaveAs(stream);

                    if (SelectFile == null && string.IsNullOrEmpty(SelectFile.PictureLink))
                    {
                        IsDoing = false;
                        return;
                    }

                    string filePath = Path.GetDirectoryName(SelectFile.PictureLink);
                    filePath = filePath + "\\"
                        + Guid.NewGuid().ToString()
                        + Path.GetExtension(SelectFile.PictureLink);
                    bool isSave = FileManager.WriteToFile(stream, filePath);

                    if (isSave)
                    {
                        _toastService.ShowSuccess("File Save Successfully");

                        await jSRuntime.InvokeVoidAsync("UnselectAll", ".selectExcelData");

                        SelectFile.PictureLink = filePath;
                        await taskService.UpdateTaskImageAsync(SelectFile);
                    }
                    else
                    {
                        _toastService.ShowSuccess("File Not Save Successfully, Please Try Again After Some Time");
                    }
                }

                await Task.Run(() => IsDoing = false);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View file error", ex);
            }
        }

        protected async Task ActiveAddDataExcelFile()
        {
            await Task.Run(() => IsAdd = true);

            CreateNewExcelRow();

            await Task.Run(() => IsAdd = false);
        }

        protected void UpdateExcelFile(IRange column, ChangeEventArgs arg)
        {
            try
            {
                if (column != null && arg != null)
                {
                    Worksheet.Range[column.AddressLocal].Value = arg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View file error", ex);
            }
        }

        protected async Task ActiveExcelEdit()
        {
            await Task.Run(() => IsUpdate = true);

            if (SelectedData != null && SelectedData.Any())
            {
                IsActiveExcelFileinputDataComponent = true;
            }

            await Task.Run(() => IsUpdate = false);
        }

        protected async Task SelectAllColumn(IRange column)
        {
            try
            {
                await Task.Run(() => IsUpdate = true);

                if (column == null)
                {
                    return;
                }

                string columnName = GetColumnName(column.AddressLocal);
                await jSRuntime.InvokeVoidAsync("SelectAllColumn", "ExcelTableId",
                columnName, "selectExcelData");

                if (SelectedData == null)
                {
                    SelectedData = new List<IRange>();
                }

                for (int i = 0; i < Worksheet.Rows.Count(); i++)
                {
                    string name = columnName + Convert.ToString(i + 1);
                    var col = Worksheet.Rows[i].Columns.FirstOrDefault(c => c.AddressLocal == name);

                    if (col != null)
                    {
                        if (SelectedData.Any(c => c.AddressLocal == col.AddressLocal))
                        {
                            SelectedData.Remove(SelectedData
                                .FirstOrDefault(c => c.AddressLocal == col.AddressLocal));
                        }
                        else
                        {
                            SelectedData.Add(col);
                        }
                    }
                }

                await Task.Run(() => IsUpdate = false);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View file error", ex);
            }
        }

        protected async Task SelectRow(IRange row)
        {
            await Task.Run(() => IsUpdate = true);

            if (row == null)
            {
                return;
            }

            try
            {
                await jSRuntime.InvokeVoidAsync("SelectRow", "ExcelTableId",
                    row.AddressLocal, "selectExcelData");

                var selectRow = Worksheet.Rows.FirstOrDefault(r => r.AddressLocal == row.AddressLocal);

                if (selectRow != null)
                {
                    if (SelectedData == null)
                    {
                        SelectedData = new List<IRange>();
                    }

                    foreach (var col in selectRow.Columns)
                    {
                        if (SelectedData.Any(c => c.AddressLocal == col.AddressLocal))
                        {
                            SelectedData.Remove(SelectedData
                                .FirstOrDefault(c => c.AddressLocal == col.AddressLocal));
                        }
                        else
                        {
                            SelectedData.Add(col);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View file error", ex);
            }

            await Task.Run(() => IsUpdate = false);
        }

        protected async Task SelectSingleData(IRange column)
        {
            await Task.Run(() => IsUpdate = true);

            if (column == null)
            {
                return;
            }

            try
            {
                await jSRuntime.InvokeVoidAsync("SelecElement", column.AddressLocal, "selectExcelData");

                if (SelectedData == null)
                {
                    SelectedData = new List<IRange>();
                }

                if (SelectedData.Any(c => c.AddressLocal == column.AddressLocal))
                {
                    SelectedData.Remove(SelectedData
                            .FirstOrDefault(c => c.AddressLocal == column.AddressLocal));
                }
                else
                {
                    SelectedData.Add(column);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View file error", ex);
            }

            await Task.Run(() => IsUpdate = false);
        }

        public async Task ExcelFileInputDataComponenetMessage(string InputData)
        {
            await Task.Run(() => IsDoing = true);

            if (!string.IsNullOrEmpty(InputData) && Worksheet != null && SelectedData != null)
            {
                foreach (var data in SelectedData)
                {
                    Worksheet.Range[data.AddressLocal].Value = InputData;
                }

                SelectedData = null;
            }

            await Task.Run(() => IsActiveExcelFileinputDataComponent = false);
            await Task.Run(() => IsDoing = false);
        }

        public void Dispose()
        {
            ExcelEngine?.Dispose();
            _fileStream?.Dispose();
        }
    }
}