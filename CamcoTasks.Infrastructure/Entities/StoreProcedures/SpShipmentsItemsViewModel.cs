// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpShipmentsItemsDetailsModel
{
    public IList<SpShipmentsItemsModel> SpShipmentsItemsDetails { get; set; }
}

public class SpShipmentsItemsModel
{
    public int Id { get; set; }

    public string Customer { get; set; }

    public string PartNumber { get; set; }

    public DateTime ShipDate { get; set; }
}