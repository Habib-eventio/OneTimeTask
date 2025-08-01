
namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum MachineStatusChoice
{
    [CustomDisplay("IDLE")] IDLE = 1,

    [CustomDisplay("DOWN")] DOWN = 2,

    [CustomDisplay("SETUP")] SETUP = 3,

    [CustomDisplay("RUNNING")] RUNNING = 4,
}