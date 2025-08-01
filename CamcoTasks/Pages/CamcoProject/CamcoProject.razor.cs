using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class CamcoProject
    {
        protected PageLoadTimeViewModel pageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "CamcoProject",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        public bool Visibility { get; set; } = false;
        protected SfTextBox PassWordTextBox { get; set; }
        protected string Password { get; set; } = "";

        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IToastService ToastService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => pageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            pageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(pageLoadTime);
        }

        protected void OpenDialog()
        {
            Visibility = true;
        }

        protected void DialogClose()
        {
            Visibility = false;
        }

        protected void OverlayClick()
        {
            Visibility = false;
        }

        protected void Keydown(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" && !string.IsNullOrEmpty(Password))
            {
                GoToThePage("/camcoProject/viewCamcoProjectCost");
            }
        }

        protected void GoToThePage(string url)
        {
            if (url == "/camcoProject/viewCamcoProjectCost")
            {
                if (!string.IsNullOrWhiteSpace(Password))
                {
                    if (Password.ToLower() == "adhk1213!")
                    {
                        NavigationManager.NavigateTo(url);
                        Visibility = false;
                    }
                    else
                    {
                        ToastService.ShowError("PLEASE CONTACT TO TASKS APP LEADER, INCORRECT PASSWORD");
                    }
                }
                else
                {
                    ToastService.ShowError("Please Enter Password.");
                }

            }
            else
            {
                NavigationManager.NavigateTo(url);
            }
        }
    }
}
