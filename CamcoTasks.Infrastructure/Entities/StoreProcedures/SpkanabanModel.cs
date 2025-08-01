using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpkanabanModel
{
    public IList<KanbanDataAllRecords> SpKanbanItems { get; set; }
}

public class KanbanDataAllRecords
{
    public string Id { get; set; }
    public string QRN { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string KanbanMinimum { get; set; }
    public string KanbanReOrderQty { get; set; }
    public string CurrentQuantity { get; set; }
    public string DateKanbaned { get; set; }
    public string PurchaseOrder { get; set; }
    public string UnitOfMeasure { get; set; }
    public string GenericId { get; set; }
    public string LastAudited { get; set; }
    public string OnOrder { get; set; }
}