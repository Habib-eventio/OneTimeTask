using System.Collections.Generic;
using System.IO;

namespace CamcoTasks.Library
{
    public static class FileLogo
    {
        public static Dictionary<string, string> FileLogoDictionary = new Dictionary<string, string>()
        {
            {".pdf", "fa-solid fa-file-pdf fa-5x" },
            {".csv", "fa-solid fa-file-csv fa-5x" },
            {".docx", "fa-solid fa-file-word fa-5x" },
            {".xlsx", "fa-regular fa-file-excel fa-5x" },
            {".xls", "fa-regular fa-file-excel fa-5x" },
            {".xlsb", "fa-regular fa-file-excel fa-5x" },
            {".txt", "fa-solid fa-file-lines fa-5x" },
            {".pptx", "fa-solid fa-file-powerpoint fa-5x" },
            {".zip", "fa-solid fa-file-zipper fa-5x" },
            {".rar", "fa-solid fa-file-zipper fa-5x" },
            {".graphml", "fa-solid fa-diagram-project fa-5x" },

        };
        public static string logo = null;

        public static string ReturnFileLogo(string path)
        {
            string fileExtention = Path.GetExtension(path);
            if (fileExtention != null && FileLogoDictionary.ContainsKey(fileExtention)) logo = FileLogoDictionary[fileExtention];
            else logo = "fa-solid fa-file fa-5x";


            return logo;
        }
    }
}
