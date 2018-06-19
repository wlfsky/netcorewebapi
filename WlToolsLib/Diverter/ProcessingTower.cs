
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
    /// 澶勭悊濉旓紝澶勭悊绠￠亾
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class ProcessingTower<TIn, TOut>
    {
        #region --ID鍜岀姸鎬?-
        /// <summary>
        /// 澶勭悊濉?閿?
        /// </summary>
        public string ProcessorKey { get; set; }
        /// <summary>
        /// 澶勭悊濉攊d
        /// </summary>
        public int ProcessorID { get; set; }
        /// <summary>
        /// 褰撳墠澶勭悊濉斾换鍔￠噺
        /// </summary>
        private long processorTaskCount = 0;
        /// <summary>
        /// 褰撳墠澶勭悊濉斾换鍔￠噺
        /// </summary>
        public long ProcessorTaskCount { get { return processorTaskCount; } }
        /// <summary>
        /// 涓嬩竴涓惊鐜仠姝㈡爣璁?
        /// </summary>
        public bool Stop { get; set; }
        /// <summary>
        /// 姣忎釜澶勭悊濉斿鐞嗙▼搴?
        /// </summary>
        public Func<TaskID<TIn>, TaskID<TOut>> ProcessHandle { get; set; }
        /// <summary>
        /// 澶勭悊濉斿畬鎴愬鐞嗗悗鍒嗗彂绋嬪簭
        /// </summary>
        public Action<TaskID<TOut>> ProcessEndHandle { get; set; }

        /// <summary>
        /// 澶勭悊濉旀瘡涓惊鐜欢杩熸绉掓暟
        /// </summary>
        private int loopDelay = 10;
        /// <summary>
        /// 鎺ㄩ€佸鐞嗘暟鎹?
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
        /// 澶勭悊鏁?1
        /// </summary>
        /// <returns></returns>
        private long Add1Count()
        {
            Interlocked.Increment(ref this.processorTaskCount);
            return Interlocked.Read(ref this.processorTaskCount);
        }
        /// <summary>
        /// 澶勭悊鏁?1
        /// </summary>
        /// <returns></returns>
        private long Sub1Count()
        {
            Interlocked.Decrement(ref this.processorTaskCount);
            return Interlocked.Read(ref this.processorTaskCount);
        }
        #endregion
        /// <summary>
        /// 澶勭悊濉旀暟鎹鍣?
        /// </summary>
        Queue<TaskID<TIn>> InList { get; set; }

        //public ConcurrentQueue<TaskID<TOut>> TaskDiverter { get; set; }

        /// <summary>
        /// 澶勭悊绋嬪簭
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
        /// 鍒濆鍖栫▼搴?
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

