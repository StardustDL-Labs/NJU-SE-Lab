using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;

namespace IACG.TagHelpers
{
    [HtmlTargetElement("timeoffset-display")]
    public class TimeOffsetDisplayTagHelper : TagHelper
    {
        public TimeOffsetDisplayTagHelper() { }

        public DateTimeOffset Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            TimeSpan tspan = DateTimeOffset.Now - Value;
            if (tspan.TotalDays > 60)
            {
                output.Content.Append(Value.ToString("F"));
            }
            else if (tspan.TotalDays > 30)
            {
                output.Content.Append($"1 月前");
            }
            else if (tspan.TotalDays > 14)
            {
                output.Content.Append($"2 周前");
            }
            else if (tspan.TotalDays > 7)
            {
                output.Content.Append($"1 周前");
            }
            else if (tspan.TotalDays > 1)
            {
                output.Content.Append($"{(int)Math.Floor(tspan.TotalDays)} 天前");
            }
            else if (tspan.TotalHours > 1)
            {
                output.Content.Append($"{(int)Math.Floor(tspan.TotalHours)} 小时前");
            }
            else if (tspan.TotalMinutes > 1)
            {
                output.Content.Append($"{(int)Math.Floor(tspan.TotalMinutes)} 分钟前");
            }
            else if (tspan.TotalSeconds > 1)
            {
                output.Content.Append($"{(int)Math.Floor(tspan.TotalSeconds)} 秒前");
            }
            else
            {
                output.Content.Append($"刚刚");
            }
            base.Process(context, output);
        }
    }
}
