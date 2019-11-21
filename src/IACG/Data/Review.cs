﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class Review
    {
        public int Id { get; set; }

        public int? AppId { get; set; }

        [Display(Name = "应用")]
        public App App { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ReviewResult Result { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset LastModifyTime { get; set; }
    }
}
