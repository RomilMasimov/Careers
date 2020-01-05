using Careers.Models.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUglify.Helpers;

namespace Careers.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "order-state", TagStructure = TagStructure.WithoutEndTag)]
    public class OrderStateTagHelper : TagHelper
    {
        public OrderStateTypeEnum OrderState { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (OrderState)
            {
                case OrderStateTypeEnum.Finished:
                    break;
                case OrderStateTypeEnum.InProcess:
                    break;
                case OrderStateTypeEnum.Canceled:
                    break;
                case OrderStateTypeEnum.WaitingForClient:
                    break;
                case OrderStateTypeEnum.WaitingForSpec:
                    break;
                case OrderStateTypeEnum.InSearchOfSpec:
                    break;
            }
        }
    }
}
