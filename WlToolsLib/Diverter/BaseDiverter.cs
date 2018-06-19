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
}

