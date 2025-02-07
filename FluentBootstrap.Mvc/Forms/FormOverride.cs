﻿using FluentBootstrap.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using FluentBootstrap.Internals;
using FluentBootstrap.Mvc.Internals;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FluentBootstrap.Mvc.Forms
{
    internal class FormOverride<TModel> : ComponentOverride<Form>
    {
        public bool HideValidationSummary { get; set; }

        protected override void OnStart(TextWriter writer)
        {
            IHtmlHelper htmlHelper = this.GetHtmlHelper<TModel>();
            // Generate the form ID if one is needed (if one was already set in the htmlAttributes, this does nothing)
            ViewContext viewContext = htmlHelper.ViewContext;
            bool flag = viewContext.ClientValidationEnabled;
            //TODO    
            //&& !viewContext. .UnobtrusiveJavaScriptEnabled;
            if (flag)
            {
                // Use a TagBuilder to generate the Id
                TagBuilder tagBuilder = new TagBuilder("form");
                string id = Component.GetAttribute("id");
                if(!string.IsNullOrWhiteSpace(id))
                {
                    tagBuilder.MergeAttribute("id", id);
                }
                tagBuilder.GenerateId(FormIdGenerator(), htmlHelper.IdAttributeDotReplacement);
                Component.MergeAttribute("id", tagBuilder.Attributes["id"]);
            }

            base.OnStart(writer);

            // Set a new form context, including a form ID if one was generated
            viewContext.FormContext = new FormContext();
            
            if (flag)
            {
                viewContext.FormContext.FormData.Add("id", Component.GetAttribute("id"));
                //viewContext.FormContext. = ;
            }
        }

        protected override void OnFinish(TextWriter writer)
        {            
            // Validation summary if it's not hidden or one was not already output
            if (!HideValidationSummary)
            {
                this.GetHelper<TModel>().ValidationSummary().GetComponent().StartAndFinish(writer);
            }

            base.OnFinish(writer);

            // Intercept the client validation (if there is any) and output on our own writer
            ViewContext viewContext = this.GetHtmlHelper<TModel>().ViewContext;
            TextWriter viewWriter = viewContext.Writer;
            viewContext.Writer = writer;
            //TODO validation
            
            //viewContext.OutputClientValidation();
            viewContext.Writer = viewWriter;

            // Clear the form context
            viewContext.FormContext = null;
        }

        private readonly static object _lastBootstrapFormNumKey = new object();

        // Get and increment a form id
        private string FormIdGenerator()
        {
            IDictionary<object,object> items = this.GetHtmlHelper<TModel>().ViewContext.HttpContext.Items;
            object item = items[_lastBootstrapFormNumKey];
            int num = (item != null ? (int)item + 1 : 0);
            items[_lastBootstrapFormNumKey] = num;
            return string.Format("bsform{0}", num);
        }
    }
}
