using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class App
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        public List<AppCategory> Categories { get; set; }
    }
}
