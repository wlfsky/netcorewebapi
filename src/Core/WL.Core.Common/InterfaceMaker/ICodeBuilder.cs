using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Common.InterfaceMaker
{
    public interface ICodeBuilder
    {
        ICodeBuilder WriteInterfaceHeaderCode(string code);
        ICodeBuilder WriteInterfaceBottomCode(string code);
        ICodeBuilder WriteMethodAttributeCode(string attribute);
        ICodeBuilder WriteMethodCode(string code);

        string Done();
    }
}
