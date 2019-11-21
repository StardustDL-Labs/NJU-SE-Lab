using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class App
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Display(Name = "所有者")]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "描述")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        public List<AppCategory> Categories { get; set; }

        [Display(Name = "上次修改时间")]
        public DateTimeOffset LastModifyTime { get; set; }
    }
}
