// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.Entities;

public class SpDisciplinesModel
{
    public IList<SpDisciplineDetailsModel> DisciplineDetails { get; set; }
}

public class SpDisciplineDetailsModel
{
    public long Id { get; set; }

    public string CreatedBy { get; set; }

    public string DepartmentName { get; set; }

    public string JobTitle { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime IncidentDate { get; set; }

    public string NatureOfViolation { get; set; }

    public long CorrectEmployeeReceivingDisciplineIdEmployeeId { get; set; }

    public string EmployeeReceivingDiscipline { get; set; }

    public string CorrectiveAction { get; set; }

    public string IncidentDescription { get; set; }

    public DateTime? NextDisciplineLevelReductionDate { get; set; }

    public string SignedDisciplineNoticeDocument { get; set; }

    public bool IsAttachmentsExist { get; set; }

    public short EnumDisciplineId { get; set; }

    public short EnumDisciplineLevelId { get; set; }

    public bool IsLocked { get; set; }
}