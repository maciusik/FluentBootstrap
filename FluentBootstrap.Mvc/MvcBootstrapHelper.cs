using FluentBootstrap.Forms;
using FluentBootstrap.Mvc.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.Mvc
{
    public class MvcBootstrapHelper<TModel> : BootstrapHelper<MvcBootstrapConfig<TModel>, CanCreate>
    {
        public MvcBootstrapHelper(IHtmlHelper<TModel> htmlHelper)
            : base(new MvcBootstrapConfig<TModel>(htmlHelper))
        {
        }
    }
}
