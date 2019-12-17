using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "placeholder-src")]
    public class ImagePlaceholder : TagHelper
    {
        public string PlaceholderSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("onerror", $"event.target.src='{PlaceholderSrc}'");
        }
    }
}