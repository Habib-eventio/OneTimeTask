namespace CamcoTasks.Infrastructure.CustomModels.Other;

public class MetricsDataCustom
{
    public string DateReported { get; set; }

    public int NumberOfGagesBehindCalibration { get; set; }

    public int NumberOfLostGages { get; set; }

    public int Total;

    public string Year;
}