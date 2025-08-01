namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum FormTypes
{
    [CustomDisplay("OUT TRANSACTION - PACK FOR UPS")]
    OutTransactionPackForUps = 1,

    [CustomDisplay("OUT TRANSACTION - PACK FOR FEDEX")]
    OutTransactionPackForFedex = 2,

    [CustomDisplay("OUT TRANSACTION - PACK FOR DHL")]
    OutTransactionPackForDhl = 3,

    [CustomDisplay("OUT TRANSACTION - PACK FOR PALLET")]
    OutTransactionPackForPallet = 4,

    [CustomDisplay("OUT TRANSACTION - PACK FOR CUSTOMER PICKUP")]
    OutTransactionPackForCustomerPickup = 5,

    [CustomDisplay("OUT TRANSACTION - PACK FOR CAMCO VEHICLE")]
    OutTransactionPackForCamcoVehicle = 6,

    [CustomDisplay("IN TRANSACTION - PUT INTO INVENTORY")]
    InTransactionPutIntoInventory = 7,

    [CustomDisplay("OUT TRANSACTION - REQUISITION FOR SHOP ORDER")]
    OutTransactionRequisitionForShopOrder = 8,

    [CustomDisplay("SO OUT FOR SUBCONTRACT")]
    SoOutForSubContract = 9,

    [CustomDisplay("RECEIVE IN UPS")] ReceiveInUps = 10,

    [CustomDisplay("RECEIVE IN FREIGHT SHIPMENT")]
    ReceiveInFreightShipment = 11,

    [CustomDisplay("RECEIVE IN CAMCO VEHICLE PICK-UP")]
    ReceiveInCamcoVehiclePickUp = 12,

    [CustomDisplay("AUDIT LOCATION")] AuditLocation = 13,

    [CustomDisplay("CLEAN")] Clean = 14,

    [CustomDisplay("OUT TRANSACTION - PACK FOR OSMI")]
    OutTransactionPackForOsmi = 15,

    [CustomDisplay("RETURN TO STOCK FROM QUALITY HOLD")]
    ReturnToStockFromQualityHold = 16,

    [CustomDisplay("PULL FOR QUALITY HOLD")]
    PullForQualityHold = 17,

    [CustomDisplay("CLOSING OUT SHIPPING BUCKET")]
    ClosingOutShippingBucket = 18,

    [CustomDisplay("PART RE-OILING")] PartReOiling = 19,

    [CustomDisplay("PUT OUT TO OSMI PARTS INTO OSMI")]
    PutOutToOsmiPartsIntoOsmi = 20,

    [CustomDisplay("PART NUMBER - AUDITS")]
    PartNumberAudit = 21,

    [CustomDisplay("OUT TRANSACTION OUT FOR QUALITY HOLD")]
    OutTransactionOutForQualityHold = 22,

    [CustomDisplay("IN TRANSACTION IN FROM OSMI")]
    InTransactionInFromOsmi = 23,

    [CustomDisplay("OSMI AUDITS")] OsmiAudits = 24,

    [CustomDisplay("IN TRANSACTION - IN TO OSMI")]
    InTransactionInToOsmi = 25,

    [CustomDisplay("RECEIVE IN DHL")] ReceiveInDhl = 26,

    [CustomDisplay("RECEIVE IN FEDEX")] ReceiveInFedex = 27,

    [CustomDisplay("RECEIVE IN OTHER")] ReceiveInOther = 28,

    [CustomDisplay("RECEIVE IN VENDOR DELIVERY")]
    ReceiveInVendorDelivery = 29
}