using System;

namespace CamcoTasks.ViewModels.MetricsGagingDataDTO
{
    public class MetricsGagingDataViewModel
    {
        public int Id { get; set; }
        public DateTime DateReported { get; set; }
        public int NumberOfGagesBehindCalibration { get; set; }
        public int NumberOfLostGages { get; set; }
        public bool? IsDeleted { get; set; }
        public override bool Equals(object obj)
        {

            return obj is MetricsGagingDataViewModel data &&
                   Id == data.Id &&
                   DateReported == data.DateReported &&
                   NumberOfGagesBehindCalibration == data.NumberOfGagesBehindCalibration &&
                   NumberOfLostGages == data.NumberOfLostGages;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DateReported, NumberOfGagesBehindCalibration, NumberOfLostGages);
        }
    }
}
