using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int AppId { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ReviewResult Result { get; set; }

        public string Comment { get; set; }
    }
}
