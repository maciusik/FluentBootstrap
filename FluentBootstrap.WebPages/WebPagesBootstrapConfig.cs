﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.WebPages
{
    //TODO No longer supported
    //public class WebPagesBootstrapConfig : BootstrapConfig
    //{
    //    internal WebPageBase WebPageBase { get; private set; }

    //    public WebPagesBootstrapConfig(WebPageBase webPageBase)
    //    {
    //        WebPageBase = webPageBase;
    //    }

    //    protected override TextWriter GetWriter()
    //    {
    //        return WebPageBase.Output;
    //    }

    //    protected override object GetItem(object key, object defaultValue)
    //    {
    //        if (WebPageBase.Context.Items.Contains(key))
    //        {
    //            return WebPageBase.Context.Items[key];
    //        }
    //        return defaultValue;
    //    }

    //    protected override void AddItem(object key, object value)
    //    {
    //        WebPageBase.Context.Items[key] = value;
    //    }
    //}
}
