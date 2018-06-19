
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
    /// 处理塔，处理管道
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class ProcessingTower<TIn, TOut>
    {
        #region --ID和状态--
        /// <summary>
        /// 处理塔 键
        /// </summary>
        public string ProcessorKey { get; set; }
        /// <summary>
        /// 处理塔id
        /// </summary>
        public int ProcessorID { get; set; }
        /// <summary>
        /// 当前处理塔任务量
        /// </summary>
        private long processorTaskCount = 0;
        /// <summary>
        /// 当前处理塔任务量
        /// </summary>
        public long ProcessorTaskCount { get { return processorTaskCount; } }
        /// <summary>
        /// 下一个循环停止标记
        /// </summary>
        public bool Stop { get; set; }
        /// <summary>
        /// 每个处理塔处理程序
        /// </summary>
        public Func<TaskID<TIn>, TaskID<TOut>> ProcessHandle { get; set; }
        /// <summary>
        /// 处理塔完成处理后分发程序
        /// </summary>
        public Action<TaskID<TOut>> ProcessEndHandle { get; set; }

        /// <summary>
        /// 处理塔每个循环延迟毫秒数
        /// </summary>
        private int loopDelay = 10;
        /// <summary>
        /// 推送处理数据
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public long PushProcess(TaskID<TIn> task)
        {
            Init();
            InList.Enqueue(task);
            return Add1Count();
        }
        /// <summary>
        /// 处理数+1
        /// </summary>
        /// <returns></returns>
        private long Add1Count()
        {
            Interlocked.Increment(ref this.processorTaskCount);
            return Interlocked.Read(ref this.processorTaskCount);
        }
        /// <summary>
        /// 处理数-1
        /// </summary>
        /// <returns></returns>
        private long Sub1Count()
        {
            Interlocked.Decrement(ref this.processorTaskCount);
            return Interlocked.Read(ref this.processorTaskCount);
        }
        #endregion
        /// <summary>
        /// 处理塔数据容器
        /// </summary>
        Queue<TaskID<TIn>> InList { get; set; }

        //public ConcurrentQueue<TaskID<TOut>> TaskDiverter { get; set; }

        /// <summary>
        /// 处理程序
        /// </summary>
        public void Process()
        {
            Init();
            var fsio00 = System.IO.File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "log0"+ this.ProcessorKey + ".txt");
            fsio00.AutoFlush = true;
            while (Stop == false)
            {
                var c = this.processorTaskCount;
                var info = "----ProcessingTower LOOP...[" + this.ProcessorKey + "]>>> " + c.ToString();
                fsio00.WriteLine(info);
                WriteLine(info);
                if (c <= 0)
                {
                    loopDelay = 2000;
                }
                else
                {
                    loopDelay = 20;
                }

                if (InList.TryDequeue(out TaskID<TIn> indata))
                {
                    Sub1Count();
                    var result = ProcessHandle(indata);
                    ProcessEndHandle(result);
                }
                Thread.Sleep(loopDelay);

            }
            fsio00.Close();
        }

        /// <summary>
        /// 初始化程序
        /// </summary>
        private void Init()
        {
            if(InList == null)
            {
                InList = new Queue<TaskID<TIn>>();
            }
            //if(TaskDiverter == null)
            //{
            //    TaskDiverter = new ConcurrentQueue<TaskID<TOut>>();
            //}
        }
    }

}

