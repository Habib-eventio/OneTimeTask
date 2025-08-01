namespace CamcoTasks.Helpers
{
	public class FiltersCriteria
	{
		public string Column { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;
		public string Condition { get; set; } = string.Empty;
		public List<string> AvailableValues { get; set; } = new();
	}
}
