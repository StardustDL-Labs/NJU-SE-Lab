using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class AppCategory
    {
        public int Id { get; set; }

        [Required]
        public int AppId { get; set; }

        public App App { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
