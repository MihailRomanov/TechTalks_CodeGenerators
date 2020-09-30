using System;
using System.Collections.Generic;
using System.Text;

public partial class CSharpTemplate : CSharpTemplateBase
{
    public CSharpTemplate(EntityModel.Entity model, string @namespace)
    {
        this._ModelField = model;
        this._NamespaceField = @namespace;
    }
}

