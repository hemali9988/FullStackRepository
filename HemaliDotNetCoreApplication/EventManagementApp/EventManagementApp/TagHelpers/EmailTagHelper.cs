using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementApp.TagHelpers
{
    [HtmlTargetElement("mailaddress",TagStructure=TagStructure.NormalOrSelfClosing)]
    public class EmailTagHelper:TagHelper
    {
        public  string MailTo { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // base.Process(context, output);
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:"+MailTo);
            output.Content.SetContent("Send mails to "+MailTo);
        }

      public  override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            var innerHtml = content.GetContent();

            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + MailTo);
            output.PreContent.SetHtmlContent("<strong>");
            output.PostContent.SetHtmlContent("</strong>");

            output.Content.SetContent(innerHtml);

        }
    }
}
