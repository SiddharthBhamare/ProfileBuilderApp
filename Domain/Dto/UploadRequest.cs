namespace ProfileBuilder.Domain.Dto
{
    public class UploadRequest
    {
        public int DashboardId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
    }
}
