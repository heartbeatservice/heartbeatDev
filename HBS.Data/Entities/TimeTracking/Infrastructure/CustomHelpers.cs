using System.Web.Mvc;

namespace HBS.Data.Entities.TimeTracking.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper helper, string navigateUrl, string actionCss, string imgSrc, string altText, string imageCss, string actionTarget = null)
        {
            var actionTag = new TagBuilder("a");
            actionTag.MergeAttribute("href", navigateUrl);
            if (!string.IsNullOrEmpty(actionCss))
                actionTag.AddCssClass(actionCss);
            if (!string.IsNullOrEmpty(actionTarget))
                actionTag.MergeAttribute("target", actionTarget);

            var imageTag = new TagBuilder("img");
            imageTag.MergeAttribute("src", imgSrc);
            if (!string.IsNullOrEmpty(altText))
                imageTag.MergeAttribute("alt", altText);
            if (!string.IsNullOrEmpty(imageCss))
                imageTag.AddCssClass(imageCss);

            actionTag.InnerHtml = imageTag.ToString();

            return MvcHtmlString.Create(actionTag.ToString());


        }

        public static MvcHtmlString GenerateSpan(this HtmlHelper helper,string name, string content, string cssClass)
        {
            var spanTag = new TagBuilder("span");
            spanTag.GenerateId(name);
            if (!string.IsNullOrEmpty(cssClass))
                spanTag.AddCssClass(cssClass);
            spanTag.SetInnerText(content);
            
            return MvcHtmlString.Create(spanTag.ToString());
        }
        public static string GenerateSpanRaw(this HtmlHelper helper, string name, string content, string cssClass)
        {
            var spanTag = new TagBuilder("span");
            spanTag.GenerateId(name);
            if (!string.IsNullOrEmpty(cssClass))
                spanTag.AddCssClass(cssClass);
            spanTag.SetInnerText(content);

            return spanTag.ToString();
        }

        public static MvcHtmlString GetMvcHtmlString(this HtmlHelper helper, string value)
        {
            return MvcHtmlString.Create(value);
        }

    }
}