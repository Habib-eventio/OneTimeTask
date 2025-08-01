using System;

namespace CamcoTasks.ViewModels.AutomatedAutomationsDTO
{
    public class AutomatedAutomationsViewModel
    {
        public int ID { get; set; }
        public string AutomationType { get; set; }
        public string Name { get; set; }
        public int TimerTick { get; set; }
        public TimeSpan EarliestTime { get; set; }
        public TimeSpan LatestTime { get; set; }
        public DateTime LastRun { get; set; }
        public bool IsEnabled { get; set; }
        public string WeekdayToRun { get; set; }
        public bool NeedsRestart { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool IsRunningNow { get; set; }
    }
}
