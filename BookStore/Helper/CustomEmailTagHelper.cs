using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public class CustomEmailTagHelper:TagHelper
    {

        public String myEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{myEmail}");
            output.Attributes.Add("id", "my-email");
            output.Content.SetContent("Email");
        }
    }
}
