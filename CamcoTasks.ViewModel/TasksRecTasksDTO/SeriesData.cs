using System.Collections.Generic;

namespace CamcoTasks.Data.ModelsViewModel
{
    public class SeriesData
    {
        public string XName { get; set; }
        public string YName { get; set; }
        public string EmployeeName { get; set; }
        public List<LineChartData> Data { get; set; }
    }

    public class LineChartData
    {
        public string XValue { get; set; }
        public double YValue { get; set; }
    }
}
