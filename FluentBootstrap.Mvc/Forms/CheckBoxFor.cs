using System;
using System.IO;
using System.Linq.Expressions;
using FluentBootstrap.Internals;
using FluentBootstrap.Mvc.Internals;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FluentBootstrap.Mvc.Forms
{
    public class CheckBoxFor<TModel, TValue> : Component
    {
        private readonly Expression<Func<TModel, TValue>> _expression;
        private readonly bool _isNameInLabel;
        private string _name;

        internal CheckBoxFor(BootstrapHelper helper, Expression<Func<TModel, TValue>> expression, bool isNameInLabel = true)
            : base(helper)
        {
            _expression = expression;
            _isNameInLabel = isNameInLabel;
        }

        protected override void OnStart(TextWriter writer)
        {
            base.OnStart(writer);

            ModelExpression modelExpression = this.GetHtmlHelper<TModel>().FromLambdaExpression(_expression);
            string expressionText = this.GetHtmlHelper<TModel>().GetExpressionText(_expression);
            _name = MvcFormExtensions.GetControlName(this.GetHelper<TModel>(), expressionText);
            string label = MvcFormExtensions.GetControlLabel(modelExpression.Metadata, expressionText);
            bool isChecked = false;
            if (modelExpression.Model == null || !bool.TryParse(modelExpression.Model.ToString(), out isChecked))
            {
                isChecked = false;
            }
            writer.Write(_isNameInLabel 
                ? GetHelper().CheckBox(_name, label, null, isChecked).AddAttribute("value", isChecked)
                : GetHelper().CheckBox(_name, null, label, isChecked).AddAttribute("value", isChecked));
        }

        protected override void OnFinish(TextWriter writer)
        {
            writer.Write(GetHelper().Hidden(_name, false));
            base.OnFinish(writer);
        }
    }
}