// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_CPARS")]
public class Cpars
{
    [Column("Date Closed", TypeName = "datetime")]
    public DateTime? DateClosed { get; set; }

    [Column("FOLDER MADE", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FolderMade { get; set; }

    [Column("Corrective Action", TypeName = "bit")]
    public bool CorrectiveAction { get; set; }

    [Column("Preventive Action", TypeName = "bit")]
    public bool PreventiveAction { get; set; }

    [Column("CPAR Recipient (s):", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CparRecipientS { get; set; }

    [Column("CPAR Requester:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CparRequester { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CPAR Number:", TypeName = "int")]
    public int Id { get; set; }

    [Column("CPAR Issue Date:", TypeName = "datetime")]
    public DateTime? CparIssueDate { get; set; }

    [Column("CPAR Due Date", TypeName = "datetime")]
    public DateTime? CparDueDate { get; set; }

    [Column("QMS nonconformance", TypeName = "bit")]
    public bool QmsNonconformance { get; set; }

    [Column("Nonconforming product", TypeName = "bit")]
    public bool NonconformingProduct { get; set; }

    [Column("Customer complaint or RMA", TypeName = "bit")]
    public bool CustomerComplaintOrRma { get; set; }

    [Column("Supplier quality issue", TypeName = "bit")]
    public bool SupplierQualityIssue { get; set; }

    [Column("Other CPAR category", TypeName = "bit")]
    public bool OtherCparCategory { get; set; }

    [Column("Other CPAR category entry", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OtherCparCategoryEntry { get; set; }

    [Column("Statement of Nonconformance or Problem (CPAR Requester)", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string StatementOfNonconformanceOrProblemCparRequester { get; set; }

    [Column("Immediate Containment Action (CPAR Recipient)", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ImmediateContainmentActionCparRecipient { get; set; }

    [Column("Root Cause (CPAR Recipient)", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string RootCauseCparRecipient { get; set; }

    [Column("Permanent Corrective Action (CPAR Recipient)", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PermanentCorrectiveActionCparRecipient { get; set; }

    [Column("Preventive Action (CPAR Recipient)", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PreventiveActionCparRecipient { get; set; }

    [Column("Verification of Effectiveness (CPAR Recipient)", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string VerificationOfEffectivenessCparRecipient { get; set; }

    [Column("Document 1", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Document1 { get; set; }

    [Column("Document 2", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Document2 { get; set; }

    [Column("Document 3", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Document3 { get; set; }

    [Column("Document 4", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Document4 { get; set; }

    [Column("CPAREditBy", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string CpareditBy { get; set; }

    [Column("CPAREditDate", TypeName = "datetime")]
    public DateTime? CpareditDate { get; set; }

    [Column("StaffChampion", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string StaffChampion { get; set; }

    [Column("ScarDoc", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ScarDoc { get; set; }

    [Column("8DDoc", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string _8ddoc { get; set; }

    [Column("OtherDoc", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string OtherDoc { get; set; }

    [Column("ScarDocReq", TypeName = "bit")]
    public bool ScarDocReq { get; set; }

    [Column("8DDocReq", TypeName = "bit")] public bool _8ddocReq { get; set; }

    [Column("OtherDocReq", TypeName = "bit")]
    public bool OtherDocReq { get; set; }

    [Column("RequesterEmpID", TypeName = "int")]
    public int? RequesterEmpId { get; set; }

    [Column("RecipientEmpID", TypeName = "int")]
    public int? RecipientEmpId { get; set; }

    [Column("ChampionEmpID", TypeName = "int")]
    public int? ChampionEmpId { get; set; }

    [Column("RecipientApprovedDate", TypeName = "datetime")]
    public DateTime? RecipientApprovedDate { get; set; }

    [Column("RequesterApprovedDate", TypeName = "datetime")]
    public DateTime? RequesterApprovedDate { get; set; }

    [Column("ChampionApprovedDate", TypeName = "datetime")]
    public DateTime? ChampionApprovedDate { get; set; }

    [Column("Vendor", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Vendor { get; set; }
}