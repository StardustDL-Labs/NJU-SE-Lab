using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public enum ReviewResult
    {
        [Display(Name = "待审")]
        Waiting,
        [Display(Name = "接受")]
        Accept,
        [Display(Name = "拒绝")]
        Reject
    }
}
