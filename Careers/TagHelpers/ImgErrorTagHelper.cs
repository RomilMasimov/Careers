using Microsoft.AspNetCore.Razor.TagHelpers;
using NUglify.Helpers;

namespace Careers.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "error-src", TagStructure = TagStructure.WithoutEndTag)]
    public class ImgErrorTagHelper : TagHelper
    {
        public string ErrorSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ErrorSrc.IsNullOrWhiteSpace()) output.Attributes.Add("onerror", "event.target.src='/images/header/nophoto.png'");
            else output.Attributes.Add("onerror", $"event.target.src='{ErrorSrc}'");
        }
    }
}
