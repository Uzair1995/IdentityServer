using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Repositories.Models
{
    public class ClientData
    {
        [Key]
        public string ClientId { get; set; }

        public string Email { get; set; }

        public string EmailTemplate { get; set; }

        public string FirmUserBelongsTo { get; set; }

        public string CorrespondentCode { get; set; }

        public string BoothId { get; set; }

        public string Office { get; set; }
    }
}
