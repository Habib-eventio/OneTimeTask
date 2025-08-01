using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages
{
	public class IndexModel : ComponentBase
	{
		protected string ViewTasksPage { get; set; } = "viewtasks";
		protected string ViewRecTasksPage { get; set; } = "viewrecurringtasks";

		[Inject]
		protected NavigationManager _navigationManager { get; set; }
		[Inject]
		private IToastService _toastService { get; set; }
		[Inject]
		protected ITasksService taskService { get; set; }
		//[Inject]
		//protected IMetricService metricService { get; set; }
		[Inject]
		private ILogger<IndexModel> _logger { get; set; }

		public int OneTimeTaskCount { get; set; } = 0;
		public int RecTaskCount { get; set; } = 0;
		public int MetricsCount { get; set; } = 0;

		protected void NavigateHomePage()
		{
			_navigationManager.NavigateTo($"/HomePage/");
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				try
				{

					OneTimeTaskCount = await taskService.GetOneTimeTasksCountAsync();

					RecTaskCount = await taskService.GetRecurringTasksCountAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Index Page Error", ex);
				}

				StateHasChanged();
			}
		}
	}
}