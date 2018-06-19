using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;// 并发容器ConcurrentQueue先进先出
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace WL.Core.ConsoleApp
{
    /// <summary>
    /// 分流器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDiverter<T>
    {
        /// <summary>
        /// 任务分流器容器
        /// </summary>
        public ConcurrentQueue<TaskID<T>> TaskDiverter { get; set; }
        /// <summary>
        /// 分流器容器内任务数量
        /// </summary>
        private long diverterCount = 0;
        /// <summary>
        /// 分流器容器内任务数量
        /// </summary>
        public long DiverterCount { get { return Interlocked.Read(ref diverterCount); } }

        /// <summary>
        /// 任务拉取处理，任务处理分发到处理集群时执行
        /// </summary>
        public Action<TaskID<T>> PullProcess { get; set; }
        /// <summary>
        /// 循环延迟时间
        /// </summary>
        protected int loopDelay = 10;
        /// <summary>
        /// 是否停止
        /// </summary>
        protected bool stop = false;

        /// <summary>
        /// 是否停止处理
        /// </summary>
        public bool IsStop { get { return stop; } }
        /// <summary>
        /// 向容器内推送任务数据
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public BaseDiverter<T> PushTask(T task)
        {
            // 无传入数据不处理
            if(task == null)
            {
                return this;
            }
            // 先检查分流容器是否空，空则创建
            if (TaskDiverter == null)
            {
                TaskDiverter = new ConcurrentQueue<TaskID<T>>();
            }
            // 创建新任务
            var new_task = new TaskID<T>() { TaskPID = 0, TaskData = task };
            TaskDiverter.Enqueue(new_task);
            Interlocked.Increment(ref this.diverterCount);
            return this;
        }
        /// <summary>
        /// 向容器内推送任务数据，数据带有任务id
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public BaseDiverter<T> PushTask(TaskID<T> task)
        {
            // 无传入数据不处理
            if (task == null)
            {
                return this;
            }
            // 先检查分流容器是否空，空则创建
            if (TaskDiverter == null)
            {
                TaskDiverter = new ConcurrentQueue<TaskID<T>>();
            }
            // 
            TaskDiverter.Enqueue(task);
            Interlocked.Increment(ref this.diverterCount);
            return this;
        }

        /// <summary>
        /// 取出任务
        /// </summary>
        /// <returns></returns>
        public TaskID<T> PullTask()
        {
            // 先检查分流容器是否空，空则创建
            if (TaskDiverter == null)
            {
                TaskDiverter = new ConcurrentQueue<TaskID<T>>();
                return null;
            }
            if (TaskDiverter.TryDequeue(out TaskID<T> out_task))
            {
                Interlocked.Decrement(ref this.diverterCount);
                return out_task;
            }
            return null;
        }

        /// <summary>
        /// 分配任务，要重写
        /// </summary>
        public virtual void Distribute()
        {
            
        }
        /// <summary>
        /// 产生id，时间ticks，能表现先后顺序
        /// </summary>
        /// <returns></returns>
        private long NewPID()
        {
            // 会发生重复，所以要做一个微型操作纯当延迟
            long x = 103453553534l;//微型操作，
            return DateTime.Now.Ticks;
        }
        /// <summary>
        /// 下一个循环停止执行
        /// </summary>
        public void Stop()
        {
            this.stop = true;
        }
    }

    /// <summary>
    /// 输入分流器
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public class InDiverter<TIn> : BaseDiverter<TIn>
    {
        /// <summary>
        /// 输入分流器具体处理
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
                Task.Run(()=> {
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

    /// <summary>
    /// 处理状态，已经融合到处理塔中了
    /// </summary>
    public class ProcessorStatus
    {
        public string ProcessorKey = "";
        public int ProcessorID = 0;
        private long processorTaskCount = 0;
        public long ProcessorTaskCount { get { return processorTaskCount; } }

        public long Add1Count()
        {
            Interlocked.Increment(ref this.processorTaskCount);
            return Interlocked.Read(ref this.processorTaskCount);
        }

        public long Sub1Count()
        {
            Interlocked.Decrement(ref this.processorTaskCount);
            return Interlocked.Read(ref this.processorTaskCount);
        }
    }


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

            InputTask.PullProcess = (t) => {
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
            Task.Run(() => {
                WriteLine(PCluster.Clusters.JoinBy(p => "[PID:{0},PCOUNT:{1}]".FormatStr(p.Key, p.Value.ProcessorTaskCount)));
                Task.Delay(2000);
                //Thread.Sleep(1000);
            });
        }
    }
}


