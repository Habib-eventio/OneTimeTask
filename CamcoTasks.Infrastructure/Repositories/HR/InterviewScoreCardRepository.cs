using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;
using CamcoTasks.Infrastructure;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class InterviewScoreCardRepository : Repository<InterviewScoreCard>,
    IInterviewScoreCardRepository
{
    public InterviewScoreCardRepository(DatabaseContext context) : base(context)
    {
    }

    private DatabaseContext DatabaseContext => (DatabaseContext)Context;
}
