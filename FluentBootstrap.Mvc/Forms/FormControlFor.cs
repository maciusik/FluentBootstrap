using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentBootstrap.Html;
using FluentBootstrap.Forms;

namespace FluentBootstrap.Mvc.Forms
{
    public class FormControlFor<TModel, TValue> : FormControlForBase<TModel, TValue>
    {
        public FormControlFor(BootstrapHelper helper, bool editor, Expression<Func<TModel, TValue>> expression)
            : base(helper, editor, expression)
        {
        }
    }
}
