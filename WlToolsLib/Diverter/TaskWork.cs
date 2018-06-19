using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;// 骞跺彂瀹瑰櫒ConcurrentQueue鍏堣繘鍏堝嚭
using System.Linq;
using System.Threading.Tasks;
using WlToolsLib.Expand;
using static System.Console;

namespace WlToolsLib.Diverter
{

    /// <summary>
    /// 浠诲姟宸ヤ綔锛屼娇鐢ㄥ鐞嗛泦缇ょ殑鍏ュ彛
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class TaskWork<TIn, TOut>
    {
        /// <summary>
        /// 骞跺彂瀹夊叏鐨勮緭鍏ュ垎娴佸櫒
        /// </summary>
        public InDiverter<TIn> InputTask { get; set; }
        /// <summary>
        /// 澶勭悊闆嗙兢
        /// </summary>
        public ProcessingCluster<TIn, TOut> PCluster { get; set; }
        /// <summary>
        /// 绾跨▼瀹夊叏鐨勮緭鍑哄垎娴佸櫒
        /// </summary>
        public OutDiverter<TOut> OutputTask { get; set; }
        /// <summary>
        /// 鍒濆鍖栵紝鏁版嵁瀹瑰櫒锛岃緭鍏ュ垎鍙戝拰杈撳嚭鍒嗗彂
        /// </summary>
        public TaskWork()
        {
            InputTask = new InDiverter<TIn>();
            PCluster = new ProcessingCluster<TIn, TOut>();
            OutputTask = new OutDiverter<TOut>();

            InputTask.PullProcess = (t) => {
                // 骞跺彂杩涘叆闃熷垪鏃朵笉棰嗗彇鏃堕棿鎴砳d锛岃€屾槸鎺掗槦鍑洪槦鍒楁椂棰嗗彇鏃堕棿鎴砳d
                t.TaskPID = DateTime.Now.Ticks;
                PCluster.PushCluster(t);
                WriteLine("INPUT TO ProcessingTower..." + t.TaskPID.ToString());
                //Thread.Sleep(5);
            };

            PCluster.ProcessEndHandle = (t) =>
            {
                OutputTask.PushTask(t);
                WriteLine("ProcessingTower TO OUTPUT..." + t.TaskPID.ToString());
                Thread.Sleep(20);
            };
        }


        /// <summary>
        /// 杩愯浠诲姟
        /// </summary>
        public void RunTask()
        {
            // InputTask.PushTask(input);
            InputTask.Distribute();
            PCluster.Process();
            OutputTask.Distribute();
        }

        /// <summary>
        /// 杈撳嚭鐘舵€?
        /// </summary>
        public void OutputStatus()
        {
            Task.Run(() => {
                WriteLine(PCluster.Clusters.JoinBy(p => "[PID:{0},PCOUNT:{1}]".FormatStr(p.Key, p.Value.ProcessorTaskCount)));
                Task.Delay(2000);
                //Thread.Sleep(1000);
            });
        }
    }

}

