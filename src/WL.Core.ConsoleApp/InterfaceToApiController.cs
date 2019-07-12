using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using C = System.Console;
using WlToolsLib.Extension;
using System.Reflection;

namespace WL.Core.ConsoleApp
{
    /// <summary>
    /// 将接口转换为apiController
    /// </summary>
    public class InterfaceToApiController
    {

        public InterfaceToApiController()
        {

        }

        public string DllPath { get; set; } = string.Empty;

        public List<string> MethodBlackList { get; set; } = new List<string>();

        public List<string> InterfaceWhiteList { get; set; } = new List<string>();
        public List<string> Attributes { get; set; } = new List<string>();
        public string ControllerName { get; set; } = "";

        public InterfaceToApiController LoadDll(string dllPath)
        {
            if (dllPath.NotNullStr())
            {
                this.DllPath = dllPath;
            }
            return this;
        }

        public InterfaceToApiController SetInterfaceName(string interfaceName)
        {
            return this;
        }

        public InterfaceToApiController SetAttrs(List<string> attrs)
        {
            if (attrs.HasItem())
            {
                Attributes.AddRange(attrs);
            }
            return this;
        }

        public InterfaceToApiController SetControllerName(string controllerName)
        {
            if (controllerName.NotNullStr())
            {
                this.ControllerName = $"{controllerName}Controller";
            }
            return this;
        }

        public InterfaceToApiController BlackMethodList(List<string> methodList)
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
            #region --q--
            Func<Type, string> gType = (gt) =>
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
            };

            Action<Type> act = (t) =>
            {
                //Type t = typeof(IAccountTest);
                if (!t.IsInterface)
                {
                    return;
                }

                var methods = t.GetMethods();

                var template = "$attrs$\rpublic $return$ $methodname$($parameters$)";
                var attrHttp = "[HttpPost]";



                foreach (var m in methods)
                {
                    if (this.MethodBlackList.HasItem() && this.MethodBlackList.Any(mm => mm.ToLower() == m.Name.ToLower()))
                    {
                        continue;
                    }
                    var ps = m.GetParameters();
                    CWL($"{attrHttp} public {gType(m.ReturnType)} {m.Name}({ps.JoinBy(p => $"{p.ParameterType.ToString()} {p.Name}", ", ")}){{ return _account.{m.Name}({ps.JoinBy(p => p.Name, ",")}); }}");
                }
            };

            #endregion
            CWL($" 打印Type ");
            ass.GetTypes().Foreach((md)=> { if (md.IsInterface) { CWL($"{md.Name}"); act(md); } else { CWL( $"{md.Name}"); } });
            CWL($" 打印 Controller ");
            
            
            return "";
        }
    }

    public class AccountReq
    {
        public string Account { get; set; }
    }

    public class AccountRes
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get; set; }
        public Address AddressInfo { get; set; }
    }

    public class Address
    {
        public string AddressStr { get; set; }
    }

    public class Role
    {
        public string RoleName { get; set; }
    }

    public interface IAccountTest
    {
        AccountRes GetAccount(AccountReq req);
        List<AccountRes> Accounts(AccountReq req);
    }
}
