using System;

namespace CamcoTasks.ViewModels.PlanningOverheadDTO
{
    public class PlanningOverheadViewModel
    {
        public int OverheadId { get; set; }
        public DateTime? OverheadDate { get; set; }
        public decimal? OverheadPerHour { get; set; }
        public double? TotalDirectHours { get; set; }
        public bool? IsDeleted { get; set; }
        public override bool Equals(object obj)
        {
            return obj is PlanningOverheadViewModel data &&
                   OverheadId == data.OverheadId &&
                   OverheadDate == data.OverheadDate &&
                   OverheadPerHour == data.OverheadPerHour &&
                   TotalDirectHours == data.TotalDirectHours;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
