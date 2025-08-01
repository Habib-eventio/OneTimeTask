namespace CamcoTasks.Library
{
    public class AppInformation
    {
        private static string logPassword = "Metrixnoor@logger";
        public static readonly string AppUrl = "https://tasks.camcomfginc.com/";
        public static readonly string Logpath = @"\\server\CamcoImages\CamcoMetrics\MetricsLog\";
        public static readonly int PerPageItem = 50;
        public static readonly string redirectUrl = "~/viewrecurringtasks";
        public static readonly string loginUrl = "/Identity/Account/Login";
        public static readonly string appCookieName = "CamcoTaskApp";
        public static readonly string jarvisUrl = "https://jarvis.camcomfginc.com/";
        public static readonly string fivesUrl = "https://5s.camcomfginc.com/";

        public static bool CheckLogerAuthentication(string password)
        {
            bool success = false;

            if(logPassword == password)
                success = true;

            return success;
        }
    }
}
