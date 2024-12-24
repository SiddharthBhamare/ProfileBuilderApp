using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Dto
{
    public class ProfileRequest
    {
        public int UserId { get; set; }

        public string Bio { get; set; }

        public string ContactInfo { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string PortfolioUrl { get; set; }
    }
    public class UpdateProfileRequest
    {
        public int ProfileId { get; set; }

        public string Bio { get; set; }

        public string ContactInfo { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string PortfolioUrl { get; set; }
    }
}
