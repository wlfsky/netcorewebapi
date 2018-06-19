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
    /// 澶勭悊闆嗙兢
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class ProcessingCluster<TIn, TOut>
    {
        /// <summary>
        /// 澶勭悊濉斿鍣紝鍙绾冲涓鐞嗗锛屽鐞嗙閬?
        /// </summary>
        public Dictionary<string, ProcessingTower<TIn, TOut>> Clusters { get; set; }
        /// <summary>
        /// 涓诲鐞嗙▼搴忥紝璧嬪€肩粰姣忎釜澶勭悊濉?
        /// </summary>
        public Func<TaskID<TIn>, TaskID<TOut>> ProcessHandle { get; set; }
        /// <summary>
        /// 澶勭悊濉斿鐞嗗畬缁撳悗鍒嗗彂绋嬪簭
        /// </summary>
        public Action<TaskID<TOut>> ProcessEndHandle { get; set; }
        /// <summary>
        /// 鏄惁鍋滄鎵ц鏍囪
        /// </summary>
        protected bool isStop = false;
        /// <summary>
        /// 鍚戝鐞嗛泦缇ゆ帹閫佸鐞嗘暟鎹?
        /// </summary>
        /// <param name="pitem"></param>
        public void PushCluster(TaskID<TIn> pitem)
        {
            Init();

            var min_saturation = MinCountTower();// 鏈€灏忛ケ鍜?
            min_saturation.PushProcess(pitem);
        }
        /// <summary>
        /// 鍒濆鍖栧鐞嗛泦缇?
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
        /// 澶勭悊鍚姩绋嬪簭
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
        /// 鑾峰彇褰撳墠鏈夋渶灏忓鐞嗛噺鐨勫鐞嗗
        /// </summary>
        /// <returns></returns>
        public ProcessingTower<TIn, TOut> MinCountTower()
        {
            return Clusters.Values.OrderBy(t => t.ProcessorTaskCount).First();
        }
        /// <summary>
        /// 鍋滄姣忎釜澶勭悊濉旂殑涓嬩竴涓伐浣滃惊鐜?
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