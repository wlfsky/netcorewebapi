using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Common.InterfaceMaker
{
    public class DefaultCodeBuilder : ICodeBuilder
    {
        public StringBuilder MethodBody { get; set; } = new StringBuilder();
        public readonly string NewLine = "\n";

        public ICodeBuilder WriteMethodAttributeCode(string attribute)
        {
            MethodBody.Append($"[{attribute}]{NewLine}");
            return this;
        }

        public ICodeBuilder WriteMethodCode(string code)
        {
            MethodBody.Append(code);
            return this;
        }

        public ICodeBuilder WriteInterfaceHeaderCode(string code)
        {
            MethodBody.Append($"{code}\n{{");
            return this;
        }

        public ICodeBuilder WriteInterfaceBottomCode(string code)
        {
            MethodBody.Append(code);
            if (!code.EndsWith('}'))
            {
                MethodBody.Append("\n}\n");
            }
            return this;
        }

        public string Done()
        {
            return this.MethodBody.ToString();
        }
    }
}
