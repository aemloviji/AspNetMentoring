using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Module3.TagHelpers
{
    [HtmlTargetElement(Attributes = "northwind-id")]
    public class NorthwindImageTagHelper : TagHelper
    {
        public string NorthwindId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();

            output.Attributes.SetAttribute("href", $"/images/{NorthwindId}");
            output.Content.SetContent(childContent.GetContent());
        }
    }
}
