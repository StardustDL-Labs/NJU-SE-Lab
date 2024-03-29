﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IACG.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IACG.Areas.Identity.Pages.Account.Manage
{
    public class ShowRecoveryCodesModel : PageModel
    {
        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet() => RecoveryCodes == null || RecoveryCodes.Length == 0 ? RedirectToPage("./TwoFactorAuthentication") : (IActionResult)Page();
    }
}
