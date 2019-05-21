using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.Model
{
    public class InterfaceModel
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string InterfaceName { get; set; }
        /// <summary>
        /// 接口标识（唯一）
        /// </summary>
        public string InterfaceID { get; set; }
        /// <summary>
        /// 接口类型
        /// </summary>
        public string InterfaceType { get; set; }
        /// <summary>
        /// 接口基础路径
        /// </summary>
        public string InterfaceBaseUrl { get; set; }
        /// <summary>
        /// 接口版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 功能名
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 功能路径
        /// </summary>
        public string FunctionPath { get; set; }
        /// <summary>
        /// 功能版本
        /// </summary>
        public int FunctionVersion { get; set; }
        /// <summary>
        /// 功能请求Json
        /// </summary>
        public string RequestJson { get; set; }
    }
}
