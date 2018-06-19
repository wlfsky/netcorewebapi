

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;// 并发容器ConcurrentQueue先进先出
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace WlToolsLib
{

    /// <summary>
    /// 任务ID
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class TaskID<TData>
    {
        /// <summary>
        /// 处理数据id
        /// </summary>
        public long TaskPID { get; set; }
        /// <summary>
        /// 处理数据
        /// </summary>
        public TData TaskData { get; set; }
    }

}

