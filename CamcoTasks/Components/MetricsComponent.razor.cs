using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamcoTasks.Components
{
	public class MetricsComponentModel : ComponentBase
	{
		[Inject]
		protected NavigationManager NavigationManager { get; set; }
		protected void NavigateComponent(int Index)
		{
			if (Index == 1)
				NavigationManager.NavigateTo("/Metrics/General");
			else if(Index == 2)
				NavigationManager.NavigateTo("/Metrics/Purchasing");
			else if (Index == 3)
				NavigationManager.NavigateTo("/Metrics/Quality");
			else if (Index == 4)
				NavigationManager.NavigateTo("/Metrics/Maintenance");
			else if (Index == 5)
				NavigationManager.NavigateTo("/Metrics/Stockroom");
			else
				NavigationManager.NavigateTo("/Metrics/Production");
		}
		
	}
}
