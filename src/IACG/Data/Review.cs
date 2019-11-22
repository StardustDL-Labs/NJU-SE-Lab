using System;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class Review
    {
        [Display(Name = "审核号")]
        public int Id { get; set; }

        public int? AppId { get; set; }

        [Display(Name = "应用")]
        public App App { get; set; }

        [Required]
        public string UserId { get; set; }

        [Display(Name = "审核者")]
        public ApplicationUser User { get; set; }

        [Display(Name = "结果")]
        public ReviewResult Result { get; set; }

        [Display(Name = "评论")]
        public string Comment { get; set; }

        [Display(Name = "上次修改时间")]
        public DateTimeOffset LastModifyTime { get; set; }
    }
}
