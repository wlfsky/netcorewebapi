
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;// 并发容器ConcurrentQueue先进先出
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace WlToolsLib.Diverter
{

    /// <summary>
    /// 输出分流器
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public class OutDiverter<TOut> : BaseDiverter<TOut>
    {
        /// <summary>
        /// 输出分流具体处理
        /// </summary>
        public override void Distribute()
        {
            Task.Run(() =>
            {
                while (!stop)
                {
                    if (this.DiverterCount <= 0)
                    {
                        loopDelay = 2000;
                    }
                    else
                    {
                        loopDelay = 20;
                    }
                    TaskID<TOut> out_data = PullTask();
                    if (out_data != null)
                    {
                        PullProcess(out_data);
                    }
                    Task.Delay(loopDelay);
                }
            });
        }
    }
}