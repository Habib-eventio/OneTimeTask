namespace CamcoTasks.Helpers
{
	public class FormatString
	{
		public static string FormatName(string fullName)
		{
			if (string.IsNullOrWhiteSpace(fullName))
			{
				return string.Empty;
			}

			fullName = fullName.Trim();

			string firstName, lastName;

			if (fullName.Contains('.'))
			{
				var parts = fullName.Split('.');
				if (parts.Length == 2)
				{
					firstName = parts[0].Trim();
					lastName = parts[1].Trim();
					return $"{lastName.ToUpper()}, {firstName.ToUpper()}";
				}
			}

			var nameParts = fullName.Split(' ');
			if (nameParts.Length < 2)
			{
				return fullName.ToUpper();
			}

			lastName = nameParts[^1];
			firstName = string.Join(" ", nameParts[..^1]); 
			return $"{lastName.ToUpper()}, {firstName.ToUpper()}";
		}
	}
}
