// This File Needs to be reviewed Still. Don't Remove this comment.

using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpFiveSOnTimeCompletionTasksModel
{
    public IList<SpFiveSOnTimeCompletionTaskModel> SpFiveSOnTimeCompletionTasks { get; set; }
}

public class SpFiveSOnTimeCompletionTaskModel
{
    public string CustomEmployeeId { get; set; }

    public string FullName { get; set; }

    public decimal FiveSOnTimeCompletionPercentage { get; set; }
}