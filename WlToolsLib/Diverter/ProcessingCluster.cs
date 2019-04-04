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
    /// 处理集群
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class ProcessingCluster<TIn, TOut>
    {
        /// <summary>
        /// 处理塔容器，可容纳多个处理塔，处理管道
        /// </summary>
        public Dictionary<string, ProcessingTower<TIn, TOut>> Clusters { get; set; }
        /// <summary>
        /// 主处理程序，赋值给每个处理塔
        /// </summary>
        public Func<TaskID<TIn>, TaskID<TOut>> ProcessHandle { get; set; }
        /// <summary>
        /// 处理塔处理完结后分发程序
        /// </summary>
        public Action<TaskID<TOut>> ProcessEndHandle { get; set; }
        /// <summary>
        /// 是否停止执行标记
        /// </summary>
        protected bool isStop = false;
        /// <summary>
        /// 向处理集群推送处理数据
        /// </summary>
        /// <param name="pitem"></param>
        public void PushCluster(TaskID<TIn> pitem)
        {
            Init();

            var min_saturation = MinCountTower();// 最小饱和
            min_saturation.PushProcess(pitem);
        }
        /// <summary>
        /// 初始化处理集群
        /// </summary>
        private void Init()
        {
            if (Clusters == null)
            {
                Clusters = new Dictionary<string, ProcessingTower<TIn, TOut>>();
            }
            if (Clusters.Count <= 0)
            {
                var pc = 5;//Environment.ProcessorCount;
                for (int i = 0; i < pc; i++)
                {
                    Clusters.Add(i.ToString(), new ProcessingTower<TIn, TOut>() { ProcessHandle = this.ProcessHandle, ProcessEndHandle = this.ProcessEndHandle, ProcessorID = i, ProcessorKey = i.ToString(), Stop = false });
                }
            }
        }
        /// <summary>
        /// 处理启动程序
        /// </summary>
        public void Process()
        {
            Init();
            foreach (var item in Clusters)
            {
                Task.Run(() =>
                {
                    item.Value.Process();
                });
            }
        }

        /// <summary>
        /// 获取当前有最小处理量的处理塔
        /// </summary>
        /// <returns></returns>
        public ProcessingTower<TIn, TOut> MinCountTower()
        {
            return Clusters.Values.OrderBy(t => t.ProcessorTaskCount).First();
        }
        /// <summary>
        /// 停止每个处理塔的下一个工作循环
        /// </summary>
        public void Stop()
        {
            this.isStop = true;
            foreach (var item in Clusters)
            {
                item.Value.Stop = this.isStop;
            }
        }
    }

}