namespace VehicleServiceManagement.API.Models.Domain
{
    public class ErrorHandler
    {
        public int ErrorId { get; set; }

        public string ComponentName{ get; set; }

        public string ErrorName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
