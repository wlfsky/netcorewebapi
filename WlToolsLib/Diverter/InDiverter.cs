
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
    /// 杈撳叆鍒嗘祦鍣?
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public class InDiverter<TIn> : BaseDiverter<TIn>
    {
        /// <summary>
        /// 杈撳叆鍒嗘祦鍣ㄥ叿浣撳鐞?
        /// </summary>
        public override void Distribute()
        {
            Task.Run(() =>
            {
                while (stop == false)
                {
                    if (this.DiverterCount <= 0)
                    {
                        loopDelay = 2000;
                    }
                    else
                    {
                        loopDelay = 20;
                    }
                    TaskID<TIn> out_data = PullTask();
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

