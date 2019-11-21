using System;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        public int AppId { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public GradeResult Result { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset LastModifyTime { get; set; }
    }
}
