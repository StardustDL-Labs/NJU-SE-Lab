using System;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class Grade
    {
        public int Id { get; set; }

        public int? AppId { get; set; }

        public App App { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public GradeResult Result { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset LastModifyTime { get; set; }
    }
}
