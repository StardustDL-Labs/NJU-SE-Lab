using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public enum ReviewResult
    {
        [Display(Name = "等待")]
        Waiting,
        [Display(Name = "接受")]
        Accept,
        [Display(Name = "拒绝")]
        Reject
    }
}
