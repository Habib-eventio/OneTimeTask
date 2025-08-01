using Syncfusion.Blazor.Charts;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksRecTasksDTO
{
    public class Graph
    {
        public int Id { get; set; }
        public string XAxisIntervalType { get; set; }
        public IntervalType XAxisIntervalName { get; set; }
    }

    public static class GraphData
    {
        public static List<Graph> GraphXAxisIntervalList { get; } = new List<Graph>()
        {
            new Graph()
            {
                Id = 1,
                XAxisIntervalName = IntervalType.Auto,
                XAxisIntervalType = "Auto"
            },
            new Graph()
            {
                Id = 2,
                XAxisIntervalName = IntervalType.Years,
                XAxisIntervalType = "Years"
            },
            new Graph()
            {
                Id = 3,
                XAxisIntervalName = IntervalType.Months,
                XAxisIntervalType = "Months"
            },
            new Graph()
            {
                Id = 4,
                XAxisIntervalName = IntervalType.Hours,
                XAxisIntervalType = "Hours"
            },
            new Graph()
            {
                Id = 5,
                XAxisIntervalName = IntervalType.Days,
                XAxisIntervalType = "Days"
            },
            new Graph()
            {
                Id = 6,
                XAxisIntervalName = IntervalType.Minutes,
                XAxisIntervalType = "Minutes"
            },
            new Graph()
            {
                Id = 7,
                XAxisIntervalName = IntervalType.Seconds,
                XAxisIntervalType = "Seconds"
            }
        };
    }
}
