using IACG.Data;
using IACG.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IACG.TagHelpers
{
    [HtmlTargetElement("review-result")]
    public class ReviewResultTagHelper : TagHelper
    {
        public ReviewResultTagHelper()
        {
        }

        static string GetStateColor(ReviewResult state)
        {
            switch (state)
            {
                case ReviewResult.Accept:
                    return "forestgreen";
                case ReviewResult.Reject:
                    return "red";
                case ReviewResult.Waiting:
                    return "#6cf";
            }
            return "black";
        }

        public ReviewResult Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("style", $"color:{GetStateColor(Value)}");

            Microsoft.AspNetCore.Mvc.Rendering.TagBuilder icon = new TagBuilder("i");
            switch (Value)
            {
                case ReviewResult.Accept:
                    icon.Attributes["class"] = "fa fa-check";
                    break;
                case ReviewResult.Reject:
                    icon.Attributes["class"] = "fa fa-times";
                    break;
                case ReviewResult.Waiting:
                    icon.Attributes["class"] = "fa fa-hourglass-half";
                    break;
            }
            output.Content.AppendHtml(icon);

            TagBuilder text = new TagBuilder("span");
            text.Attributes["style"] = "margin-left: 10px";
            text.InnerHtml.Append(DisplayHelper.GetDisplayNameOfEnumValue(Value));
            output.Content.AppendHtml(text);
            base.Process(context, output);
        }
    }
}
