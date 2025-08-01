using CamcoTasks.ViewModels.EmployeeDTO;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.JobDescriptions
{
    public class JobDescriptionEmployeeEmail
    {
        public long Id { get; set; }
        public string JobName { get; set; }
        public bool IsSelected { get; set; } = false;
        public List<EmployeeEmail> EmployeeEmails { get; set; }


        public JobDescriptionEmployeeEmail()
        {
            EmployeeEmails = new List<EmployeeEmail>();
        }
    }
}
