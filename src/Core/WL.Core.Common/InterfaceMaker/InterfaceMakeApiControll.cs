using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WlToolsLib.Extension;
using C = System.Console;

namespace WL.Core.Common.InterfaceMaker
{
    /// <summary>
    /// 将接口转换为apiController
    /// </summary>
    public class InterfaceMakeApiControll
    {

        public InterfaceMakeApiControll()
        {
            this.MethodAttributes.Add("HttpPost");// 默认是HttpPost
        }

        /// <summary>
        /// dll 路径
        /// </summary>
        public string DllPath { get; set; } = string.Empty;
        /// <summary>
        /// 基类名
        /// </summary>
        public string BaseClassName { get; set; } = string.Empty;
        /// <summary>
        /// 基接口名列表
        /// </summary>
        public List<string> BaseInterface { get; set; } = new List<string>();

        /// <summary>
        /// 方法黑名单，不构成
        /// </summary>
        public List<string> MethodBlackList { get; set; } = new List<string>();

        /// <summary>
        /// 接口白名单，有此接口名才转化
        /// </summary>
        public List<string> InterfaceWhiteList { get; set; } = new List<string>();
        public bool OpenInterfaceWhiteList { get; set; } = false;
        /// <summary>
        /// 控制器特性列表
        /// </summary>
        public List<string> ControllerAttributes { get; set; } = new List<string>();
        /// <summary>
        /// 方法特性列表
        /// </summary>
        public List<string> MethodAttributes { get; set; } = new List<string>();
        /// <summary>
        /// 控制器名
        /// </summary>
        public string ControllerName { get; set; } = "";
        public string InterfaceInstanceName { get; set; } = "";

        public ICodeBuilder CodeBuilder { get; set; } = new DefaultCodeBuilder();

        public InterfaceMakeApiControll LoadDll(string dllPath)
        {
            if (dllPath.NotNullStr())
            {
                this.DllPath = dllPath;
            }
            return this;
        }

        public InterfaceMakeApiControll SetInterfaceName(string interfaceName)
        {
            return this;
        }

        public InterfaceMakeApiControll SetAttrs(List<string> attrs)
        {
            if (attrs.HasItem())
            {
                ControllerAttributes.AddRange(attrs);
            }
            return this;
        }

        public InterfaceMakeApiControll SetControllerName(string controllerName)
        {
            if (controllerName.NotNullStr())
            {
                this.ControllerName = $"{controllerName}Controller";
            }
            return this;
        }

        public InterfaceMakeApiControll BlackMethodList(List<string> methodList)
        {
            if (methodList.HasItem())
            {
                this.MethodBlackList.AddRange(methodList);
            }
            return this;
        }

        public string Execute()
        {
            Action<string> CWL = C.WriteLine;
            Assembly ass = Assembly.LoadFile(this.DllPath);

            ass.GetTypes().Foreach((md) =>
            {
                if (md.IsInterface)
                {
                    if (this.OpenInterfaceWhiteList)
                    {
                        if (md.Name.In(InterfaceWhiteList))
                        {
                            InterfaceMethods(md);
                        }
                    }
                    else
                    {
                        InterfaceMethods(md);
                    }
                }

            });
            CodeBuilder.WriteInterfaceBottomCode("/*--END--*/");
            return CodeBuilder.Done();
        }

        #region ----
        // 获取类型名字
        public string TypeName(Type gt)
        {
            var resStr = "";
            if (gt.IsGenericType)
            {
                resStr = $"{gt.FullName.IndexOfLeft("`")}<{gt.GenericTypeArguments.JoinBy(g => g.Name, ",")}>";
            }
            else
            {
                resStr = $"{gt.FullName}";
            }
            return resStr;
        }

        /// <summary>
        /// 接口类型内方法转控制器
        /// </summary>
        /// <param name="type"></param>
        public void InterfaceMethods(Type type)
        {
            //Type t = typeof(IAccountTest);
            if (!type.IsInterface)
            {
                return;
            }

            var t = type; // 别名
            var methods = t.GetMethods();
            var interfaceName = t.Name;

            var template = "$attrs$\rpublic $return$ $methodname$($parameters$)";

            CodeBuilder.WriteInterfaceHeaderCode($"public class {interfaceName.Slice(s:1)}Controller :  BaseApiController, {interfaceName}");

            string interfaceInstanceName = $"_{interfaceName.ToLower()}";

            foreach (var m in methods)
            {
                if (m.Name.In(this.MethodBlackList, StringExpand.SameCasePredicate))
                {
                    continue;
                }
                var ps = m.GetParameters();
                foreach (var attribute in this.MethodAttributes)
                {
                    CodeBuilder.WriteMethodAttributeCode(attribute);
                }
                // 每个方法获取接口方法上的路由特性值
                var d = m.GetCustomAttribute<ControllerRouteAttribute>();
                if (d.NotNull())
                {
                    CodeBuilder.WriteMethodAttributeCode($"Route(\"{d.Route}\")");
                }
                CodeBuilder.WriteMethodCode($"public {TypeName(m.ReturnType)} {m.Name}({ps.JoinBy(p => $"{p.ParameterType.ToString()} {p.Name}", ", ")}){{ return {interfaceInstanceName}.{m.Name}({ps.JoinBy(p => p.Name, ",")}); }}");
            }
        }
        #endregion
    }
}
