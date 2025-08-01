//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure.Entities.Kanban;
//using ERP.Data.Entities.Kanban;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Production;

//[Table("Production_PartMaster")]
//public class PartMaster
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("CAMCO PART NUMBER", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("PM_PART", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Required]
//    public string PartNumber { get; set; }

//    [Column("ENG_PRINT_REV", TypeName = "nvarchar(50)")]
//    [StringLength(50)]
//    [MaxLength(50)]
//    public string EngineeringPrintRevision { get; set; }

//    [Column("PM_DESC", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string Description { get; set; }

//    [Column("PM_CUST_CODE", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string CustomerCode { get; set; }

//    [Column("PM_PART_LENGTH", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string PartLength { get; set; }

//    [Column("PM_PART_PRICE", TypeName = "money")]
//    public decimal? PartPrice { get; set; }

//    [Column("PM_KANBAN", TypeName = "bit")]
//    public bool IsKanban { get; set; }

//    [Column("PM_TERMS", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Terms { get; set; }

//    [Column("PM_OILFREQ", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string OilFrequency { get; set; }

//    [Column("PM_SHIPPINGMETHOD", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string ShippingMethod { get; set; }

//    [Column("PM_COORDINATOR", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string Coordinator { get; set; }

//    [Column("PM_CUST_CODE2", TypeName = "int")]
//    public int? CustomerCode2 { get; set; }

//    [Column("PM_CUST_CODE3", TypeName = "int")]
//    public int? CustomerCode3 { get; set; }

//    [Column("PM_GROUP", TypeName = "int")] public int? Group { get; set; }

//    [Column("PM_LEADTIME", TypeName = "float")]
//    public double? LeadTime { get; set; }

//    [Obsolete("Not Being Used anymore, As per Ian")]
//    [Column("PM_REVLEVEL", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string RevisionLevel { get; set; }

//    [Column("GradeId", TypeName = "int")] public int? GradeId { get; set; }

//    [Column("ConditionId", TypeName = "int")]
//    public int? ConditionId { get; set; }

//    [Column("PartNickname", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string PartNickname { get; set; }

//    [Column("PartTypeId", TypeName = "int")]
//    public int? PartTypeId { get; set; }

//    public virtual ICollection<KanbanNote> KanbanNotes { get; set; } = new List<KanbanNote>();

//    public virtual ICollection<CustomerCalculatedKanban> CustomerCalculatedKanbans { get; set; } =
//        new List<CustomerCalculatedKanban>();

//    public virtual ICollection<KanbanTransitionStatus> KanbanTransitionStatus { get; set; } =
//        new List<KanbanTransitionStatus>();
//}