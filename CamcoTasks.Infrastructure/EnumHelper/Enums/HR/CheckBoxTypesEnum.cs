
namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum CheckBoxTypesEnum
{
    [CustomDisplay("IsPerProductionSupervisor")]
    IsPerProductionSupervisor = 1,

    [CustomDisplay("IsLineInspection")] IsLineInspection = 2,

    [CustomDisplay("IsProductionHoldOrDelay")]
    IsProductionHoldOrDelay = 3,

    [CustomDisplay("IsSameDayShip")] IsSameDayShip = 4,

    [CustomDisplay("IsOtherItems")] IsOtherItems = 5,

    [CustomDisplay("IsSetupSheet")] IsSetupSheet = 6,

    [CustomDisplay("IsCheckSheet")] IsCheckSheet = 7,

    [CustomDisplay("IsOds")] IsOds = 8,

    [CustomDisplay("IsGageCalibrated")] IsGageCalibrated = 9,

    [CustomDisplay("IsGagingWithLessTolerance")]
    IsGagingWithLessTolerance = 10,

    [CustomDisplay("IsUnacceptablePartFinish")]
    IsUnacceptablePartFinish = 11,

    [CustomDisplay("IsUnacceptableSharpEdges")]
    IsUnacceptableSharpEdges = 12,

    [CustomDisplay("IsFirstArticle")] IsFirstArticle = 13,

    [CustomDisplay("IsHotList")] IsHotList = 14,

    [CustomDisplay("IsProcessProveOut")] IsProcessProveOut = 15,

    [CustomDisplay("IsCmmRequired")] IsCmmRequired = 16,

}