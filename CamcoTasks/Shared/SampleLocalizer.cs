using Syncfusion.Blazor;

namespace CamcoTasks.Shared
{
    public class SampleLocalizer : ISyncfusionStringLocalizer
    {

        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return CamcoTasks.Resources.SfResources.ResourceManager;;
            }
        }
    }
}