using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.Entities;
public class ColumnForTableModel
{
    public string Name { get; set; }
    
    public string Width { get; set; }
    
    public string ColSpanCount { get; set; }
    
    public string CellDataAlignment { get; set; } = "left";
    
    public string CellHeight { get; set; } = "30";
    
    public string CellStyles { get; set; } = "";
}

public class MainRowForTableModel
{
    public List<RowDataForTableModel> RowsData { get; set; }
}

public class RowDataForTableModel
{
    public string CellDataAlignment { get; set; } = "left";

    public string CellBackGroundColor { get; set; } = "#ffffff";
    
    public string Data { get; set; }
}