// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.Production;

namespace CamcoTasks.Infrastructure.IRepository.Production;

public interface IProductionTimeSheetRepository : IRepository<ProductionTimeSheet>
{
    /// <summary>
    /// Caution: This Method is being used in FirstArticleDb Project
    /// </summary>
    //SpProductionSheetsDataModel GetSpInformationViaProductionTimeSheetsAndHelpingData();
}