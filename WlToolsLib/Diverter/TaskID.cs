

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;// 骞跺彂瀹瑰櫒ConcurrentQueue鍏堣繘鍏堝嚭
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace WlToolsLib.Diverter
{

    /// <summary>
    /// 浠诲姟ID
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class TaskID<TData>
    {
        /// <summary>
        /// 澶勭悊鏁版嵁id
        /// </summary>
        public long TaskPID { get; set; }
        /// <summary>
        /// 澶勭悊鏁版嵁
        /// </summary>
        public TData TaskData { get; set; }
    }

}

