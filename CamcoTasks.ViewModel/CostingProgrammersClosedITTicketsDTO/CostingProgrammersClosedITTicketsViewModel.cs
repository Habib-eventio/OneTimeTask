namespace CamcoTasks.ViewModels.CostingProgrammersClosedITTicketsDTO
{
    public class CostingProgrammersClosedITTicketsViewModel
    {
        public int Id { get; set; }
        public string ProgrammerName { get; set; }
        public string WeekCloseDate { get; set; }
        public int? ClosedIttickets { get; set; }
        public int? OpenToPendingReviewIttickets { get; set; }
    }
}
