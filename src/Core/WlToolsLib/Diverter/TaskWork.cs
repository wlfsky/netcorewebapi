using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;// 并发容器ConcurrentQueue先进先出
using System.Linq;
using System.Threading.Tasks;
using static System.Console;
using WlToolsLib.Extension;

namespace WlToolsLib.Diverter
{

    /// <summary>
    /// 任务工作，使用处理集群的入口
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class TaskWork<TIn, TOut>
    {
        /// <summary>
        /// 并发安全的输入分流器
        /// </summary>
        public InDiverter<TIn> InputTask { get; set; }
        /// <summary>
        /// 处理集群
        /// </summary>
        public ProcessingCluster<TIn, TOut> PCluster { get; set; }
        /// <summary>
        /// 线程安全的输出分流器
        /// </summary>
        public OutDiverter<TOut> OutputTask { get; set; }
        /// <summary>
        /// 初始化，数据容器，输入分发和输出分发
        /// </summary>
        public TaskWork()
        {
            InputTask = new InDiverter<TIn>();
            PCluster = new ProcessingCluster<TIn, TOut>();
            OutputTask = new OutDiverter<TOut>();

            InputTask.PullProcess = (t) =>
            {
                // 并发进入队列时不领取时间戳id，而是排队出队列时领取时间戳id
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
        /// 运行任务
        /// </summary>
        public void RunTask()
        {
            // InputTask.PushTask(input);
            InputTask.Distribute();
            PCluster.Process();
            OutputTask.Distribute();
        }

        /// <summary>
        /// 输出状态
        /// </summary>
        public void OutputStatus()
        {
            Task.Run(() =>
            {
                WriteLine(PCluster.Clusters.JoinBy(p => "[PID:{0},PCOUNT:{1}]".FormatStr(p.Key, p.Value.ProcessorTaskCount)));
                Task.Delay(2000);
                //Thread.Sleep(1000);
            });
        }
    }

}

