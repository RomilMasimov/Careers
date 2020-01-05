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
                    output.Attributes.Add("src", $"/images/content/finished.png");
                    break;
                case OrderStateTypeEnum.InProcess:
                    output.Attributes.Add("src", $"/images/content/inprocess.png");
                    break;
                case OrderStateTypeEnum.Canceled:
                    output.Attributes.Add("src", $"/images/content/canceled.png");
                    break;
                case OrderStateTypeEnum.WaitingForClient:
                    output.Attributes.Add("src", $"/images/content/waitingforclient.png");
                    break;
                case OrderStateTypeEnum.WaitingForSpec:
                    output.Attributes.Add("src", $"/images/content/waitingforspec.png");
                    break;
                case OrderStateTypeEnum.InSearchOfSpec:
                    output.Attributes.Add("src", $"/images/content/insearchofspec.png");
                    break;
            }
        }
    }
}
