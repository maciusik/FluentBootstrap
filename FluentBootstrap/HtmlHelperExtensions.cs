using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentBootstrap.Internals
{
    public static class HtmlHelperExtensions
    {
        public static string GetExpressionText<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            ModelExpressionProvider expressionProvider = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            return expressionProvider.GetExpressionText(expression);
        }

        public static ModelExpression FromLambdaExpression<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            ModelExpressionProvider expressionProvider = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            return expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);
        }

        public static ModelExpression FromStringExpression<TModel>(
           this IHtmlHelper<TModel> htmlHelper,
           string expression)
        {
            ModelExpressionProvider expressionProvider = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            return expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);
        }

        public static IHtmlContent GetVldAttrs<TModel>(this IHtmlHelper html)
        {
            var res = string.Empty;
            var attributeProvider = html.ViewContext.HttpContext.RequestServices.GetRequiredService<ValidationHtmlAttributeProvider>();

            foreach (var prop in typeof(TModel).GetProperties())
            {
                var clientAttributes = new Dictionary<string, string>();

                var modelExplorer = html.MetadataProvider.GetModelExplorerForType(typeof(TModel), null);
                attributeProvider.AddValidationAttributes(html.ViewContext, modelExplorer.GetExplorerForProperty(prop.Name), clientAttributes);

                res += JsonConvert.SerializeObject(clientAttributes); // like doing console.log
            }

            return new HtmlString(JsonConvert.SerializeObject(res));
        }


        public static string GenerateUrl(
            this IHtmlHelper htmlHelper,
            string route,
            RouteValueDictionary values = null)
        {
            IUrlHelper urlHelper = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();
            return urlHelper.RouteUrl(route, values);
        }
        public static string GenerateUrl(
            this IHtmlHelper htmlHelper,
            string action,
            string controller,
            RouteValueDictionary values = null)
        {
            IUrlHelper urlHelper = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();
            return urlHelper.Action(action, controller, values);
        }

        public static void GetUnobtrusiveValidationAttributes<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string name,
            MergeableDictionary attributes)
        {
            var modelExplorer = htmlHelper.FromStringExpression(name).ModelExplorer;
            var validator = htmlHelper.ViewContext.HttpContext.RequestServices.GetService<ValidationHtmlAttributeProvider>();
            validator?.AddAndTrackValidationAttributes(htmlHelper.ViewContext, modelExplorer, name, attributes.Dictionary);
        }
        
    }

   
}
