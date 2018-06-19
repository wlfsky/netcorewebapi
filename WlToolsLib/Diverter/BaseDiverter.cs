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
    /// 鍒嗘祦鍣?
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDiverter<T>
    {
        /// <summary>
        /// 浠诲姟鍒嗘祦鍣ㄥ鍣?
        /// </summary>
        public ConcurrentQueue<TaskID<T>> TaskDiverter { get; set; }
        /// <summary>
        /// 鍒嗘祦鍣ㄥ鍣ㄥ唴浠诲姟鏁伴噺
        /// </summary>
        private long diverterCount = 0;
        /// <summary>
        /// 鍒嗘祦鍣ㄥ鍣ㄥ唴浠诲姟鏁伴噺
        /// </summary>
        public long DiverterCount { get { return Interlocked.Read(ref diverterCount); } }

        /// <summary>
        /// 浠诲姟鎷夊彇澶勭悊锛屼换鍔″鐞嗗垎鍙戝埌澶勭悊闆嗙兢鏃舵墽琛?
        /// </summary>
        public Action<TaskID<T>> PullProcess { get; set; }
        /// <summary>
        /// 寰幆寤惰繜鏃堕棿
        /// </summary>
        protected int loopDelay = 10;
        /// <summary>
        /// 鏄惁鍋滄
        /// </summary>
        protected bool stop = false;

        /// <summary>
        /// 鏄惁鍋滄澶勭悊
        /// </summary>
        public bool IsStop { get { return stop; } }
        /// <summary>
        /// 鍚戝鍣ㄥ唴鎺ㄩ€佷换鍔℃暟鎹?
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public BaseDiverter<T> PushTask(T task)
        {
            // 鏃犱紶鍏ユ暟鎹笉澶勭悊
            if(task == null)
            {
                return this;
            }
            // 鍏堟鏌ュ垎娴佸鍣ㄦ槸鍚︾┖锛岀┖鍒欏垱寤?
            if (TaskDiverter == null)
            {
                TaskDiverter = new ConcurrentQueue<TaskID<T>>();
            }
            // 鍒涘缓鏂颁换鍔?
            var new_task = new TaskID<T>() { TaskPID = 0, TaskData = task };
            TaskDiverter.Enqueue(new_task);
            Interlocked.Increment(ref this.diverterCount);
            return this;
        }
        /// <summary>
        /// 鍚戝鍣ㄥ唴鎺ㄩ€佷换鍔℃暟鎹紝鏁版嵁甯︽湁浠诲姟id
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public BaseDiverter<T> PushTask(TaskID<T> task)
        {
            // 鏃犱紶鍏ユ暟鎹笉澶勭悊
            if (task == null)
            {
                return this;
            }
            // 鍏堟鏌ュ垎娴佸鍣ㄦ槸鍚︾┖锛岀┖鍒欏垱寤?
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
        /// 鍙栧嚭浠诲姟
        /// </summary>
        /// <returns></returns>
        public TaskID<T> PullTask()
        {
            // 鍏堟鏌ュ垎娴佸鍣ㄦ槸鍚︾┖锛岀┖鍒欏垱寤?
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
        /// 鍒嗛厤浠诲姟锛岃閲嶅啓
        /// </summary>
        public virtual void Distribute()
        {

        }
        /// <summary>
        /// 浜х敓id锛屾椂闂磘icks锛岃兘琛ㄧ幇鍏堝悗椤哄簭
        /// </summary>
        /// <returns></returns>
        private long NewPID()
        {
            // 浼氬彂鐢熼噸澶嶏紝鎵€浠ヨ鍋氫竴涓井鍨嬫搷浣滅函褰撳欢杩?
            long x = 103453553534l;//寰瀷鎿嶄綔锛?
            return DateTime.Now.Ticks;
        }
        /// <summary>
        /// 涓嬩竴涓惊鐜仠姝㈡墽琛?
        /// </summary>
        public void Stop()
        {
            this.stop = true;
        }
    }
}

