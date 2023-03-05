using FluentBootstrap.Forms;
using FluentBootstrap.Internals;
using FluentBootstrap.Mvc.Internals;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.Mvc.Forms
{
    internal class FormControlOverride<TModel> : ComponentOverride<FormControl>
    {
        protected override void OnStart(TextWriter writer)
        {
            IHtmlHelper<TModel> htmlHelper = this.GetHtmlHelper<TModel>();
            string name = Component.GetAttribute("name");
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Use a TagBuilder to generate the Id
                TagBuilder tagBuilder = new TagBuilder("form");
                string id = Component.GetAttribute("id");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    tagBuilder.MergeAttribute("id", id);
                }
                tagBuilder.GenerateId(name, htmlHelper.IdAttributeDotReplacement);
                Component.MergeAttribute("id", tagBuilder.Attributes["id"]);
            }

            Component.Prepare(writer);

            // Add the validation data
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Set the validation class
                MvcBootstrapConfig<TModel> config = (MvcBootstrapConfig<TModel>)Config;
                if (htmlHelper.ViewData.ModelState.TryGetValue(name, out ModelStateEntry modelState) && modelState.Errors.Count > 0)
                {
                    Component.CssClasses.Add(HtmlHelper.ValidationInputCssClassName);
                }

                // Add other validation attributes
                htmlHelper.GetUnobtrusiveValidationAttributes(name, Component.Attributes);
                //Component.MergeAttributes<string, object>();
            }

            base.OnStart(writer);
        }
    }
}
