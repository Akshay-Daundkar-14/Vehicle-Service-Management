using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public FeedbackRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateFeedbackAsync(Feedback feedback)
        {
            await _appDbContext.Feedbacks.AddAsync(feedback);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(Feedback feedback)
        {
            feedback.IsDeleted = true;
            _appDbContext.Entry(feedback).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            var result = await _appDbContext.Feedbacks.ToListAsync();
            return result;
        }

        public async Task<Feedback> GetFeedbackAsync(int id)
        {
            var feedback = await _appDbContext.Feedbacks.FindAsync(id);
            return feedback;
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _appDbContext.Entry(feedback).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
