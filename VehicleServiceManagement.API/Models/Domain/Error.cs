namespace VehicleServiceManagement.API.Models.Domain
{
    public class Error
    {
        public int ErrorId { get; set; }

        public string Message{ get; set; }

        public string Source { get; set; }

        public string StaclTrace { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
