using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages
{
    public class HomePageModel : ComponentBase
    {
        [Parameter]
        public string ComponenentIndex { get; set; }
        public bool IsShow { get; set; }
        protected string ChosenComponent { get; set; } = "1";
        protected override async Task OnParametersSetAsync()
        {
            ChosenComponent = ComponenentIndex;
            await Task.Delay(5);
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (string.IsNullOrEmpty(ComponenentIndex))
                {
                    ChosenComponent = "1";
                    await Task.Delay(5);
                    StateHasChanged();
                }
            }
        }

        protected async Task NavigateComponent(int Index)
        {
            ChosenComponent = Index.ToString();
            await Task.Delay(5);
            StateHasChanged();
        }

        public void ShowHideButton(bool Show)
        {
            IsShow = Show;
        }
    }
}