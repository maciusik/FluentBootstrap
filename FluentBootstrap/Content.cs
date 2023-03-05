using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace FluentBootstrap
{
    public class Content : Component
    {
        private readonly object _content;

        internal Content(BootstrapHelper helper, object content)
            : base(helper)
        {
            _content = content;
        }

        protected override void OnStart(TextWriter writer)
        {
            base.OnStart(writer);
            HtmlString htmlString = _content as HtmlString;
            writer.Write(htmlString != null ? htmlString.ToString() : System.Net.WebUtility.HtmlEncode(_content.ToString()));
        }
    }
}
