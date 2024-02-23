using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IFeedbackRepository
    {
        public Task<List<Feedback>> GetAllFeedbackAsync();

        public Task<Feedback> GetFeedbackAsync(int id);


        public Task CreateFeedbackAsync(Feedback feedback);

        public Task UpdateFeedbackAsync(Feedback feedback);


        public Task DeleteFeedbackAsync(Feedback feedback);
    }
}
