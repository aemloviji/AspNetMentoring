using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Module3.HtmlHelpers
{
    public static class NorthwindImageLinkHelper
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper helper, int imageId, string linkText)
        {
            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            if (linkText == null)
            {
                throw new ArgumentNullException(nameof(linkText));
            }

            var imageUrl = $"/images/{imageId}";

            var anchor = new TagBuilder("a");
            anchor.InnerHtml.Append(linkText);
            anchor.MergeAttribute("href", imageUrl);

            return anchor;
        }
    }
}
