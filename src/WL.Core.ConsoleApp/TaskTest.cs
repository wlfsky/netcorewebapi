using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Extension;

namespace WL.Core.ConsoleApp
{
    public class TaskTest
    {
        /// <summary>
        /// 任务1，先打印，再等待，接着返回
        /// </summary>
        /// <returns></returns>
        public static async Task<int> DoTask1()
        {
            Console.WriteLine("Task 1 1000");
            await Task.Delay(5000);
            return 1;
        }

        /// <summary>
        /// 任务2，先打印，再等待，接着返回
        /// </summary>
        /// <returns></returns>
        public static async Task<int> DoTask2()
        {
            Console.WriteLine("Task 2 2000");
            await Task.Delay(2000);
            return 2;
        }

        /// <summary>
        /// 任务3，先打印，再等待延迟，再得到任务1的结果，await在方法调用外
        /// </summary>
        /// <param name="t1"></param>
        /// <returns></returns>
        public static async Task<int> DoTask3UseResultOfTask1(int t1)
        {
            Console.WriteLine("Task 3 3000");
            await Task.Delay(3000);
            return t1 + 3;
        }

        /// <summary>
        /// 任务4，先打印，再等待延迟，再得到任务2的结果，await在方法调用外
        /// </summary>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static async Task<int> DoTask4UseResultOfTask2(int t2)
        {
            Console.WriteLine("Task 4 4000");
            await Task.Delay(4000);
            return t2 + 4;
        }

        /// <summary>
        /// 任务4-1，先打印，再等待延迟，再得到任务1的结果，
        /// 这里不await，而是定义结果处理
        /// 直到最外层等待时才计算
        /// </summary>
        /// <param name="t1"></param>
        /// <returns></returns>
        public static async Task<int> DoTask3UseResultOfTask11(Task<int> t1)
        {
            Console.WriteLine("Task 3 3000");
            int x = t1.Result + 3;
            Console.WriteLine("Task 3 Result");
            await Task.Delay(3000);
            Console.WriteLine("Task 3 3000  END");
            return x;
        }

        /// <summary>
        /// 任务4-2，先打印，再等待延迟，再得到任务2的结果，
        /// 这里不await，而是定义结果处理
        /// 直到最外层等待时才计算
        /// </summary>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static async Task<int> DoTask4UseResultOfTask21(Task<int> t2)
        {
            Console.WriteLine("Task 4 4000");
            await Task.Delay(4000);
            return t2.Result + 4;
        }

        /// <summary>
        /// 任务5 先打印，在等待，最后计算参数结果
        /// </summary>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <returns></returns>
        public static async Task<int> DoTask5(int t3, int t4)
        {
            Console.WriteLine("Task 5 5000");
            await Task.Delay(5000);
            return t3 + t4;
        }

        public static async Task<int> DoTask51(Task<int> t3, Task<int> t4)
        {
            Console.WriteLine("Task 5 5000");
            await Task.Delay(5000);
            return t3.Result + t4.Result;
        }

        /// <summary>
        /// 流程1，任务1，2并行，等长的完毕，任务3，4并行等长的完毕，
        /// 任务5用任务3，4的结果
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ComplexWorkFlow()
        {
            Console.WriteLine("ComplexWorkFlow 1 ------------");
            Task<int> task1 = DoTask1();
            Task<int> task2 = DoTask2();
            Task<int> task3 = DoTask3UseResultOfTask1(await task1);
            Task<int> task4 = DoTask4UseResultOfTask2(await task2);
            return await DoTask5(await task3, await task4);
            // 实验修改.上面代码可浓缩为下面？不确定
            // return await DoTask5(await DoTask3UseResultOfTask1(await DoTask1()), await DoTask4UseResultOfTask2(await DoTask2()));
        }

        /// <summary>
        /// 任务1先走，任务3，2并行，任务4最后，完结任务5
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ComplexWorkFlow2()
        {
            Console.WriteLine("ComplexWorkFlow2 2 ------------");
            // 实验修改.上面代码可浓缩为下面？不确定
            Task<int> task3 = DoTask3UseResultOfTask1(await DoTask1());
            Task<int> task4 = DoTask4UseResultOfTask2(await DoTask2());
            return await DoTask5(await task3, await task4);
        }

        /// <summary>
        /// 任务1，2，3，4同时启动 34得到12结果后再计算，再传给给5
        /// 如果不必等待，那么就用 ×.Result
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ComplexWorkFlow21()
        {
            Console.WriteLine("ComplexWorkFlow2 2 ------------");
            // 实验修改.上面代码可浓缩为下面？不确定
            Task<int> task3 = DoTask3UseResultOfTask11(DoTask1());
            Task<int> task4 = DoTask4UseResultOfTask21(DoTask2());
            return await DoTask5(await task3, await task4);
        }

        public static async Task<int> ComplexWorkFlow22()
        {
            Console.WriteLine("ComplexWorkFlow2 2 ------------");
            // 实验修改.上面代码可浓缩为下面？不确定
            Task<int> task3 = DoTask3UseResultOfTask11(DoTask1());
            Task<int> task4 = DoTask4UseResultOfTask21(DoTask2());
            return await DoTask51(task3, task4);
        }

        /// <summary>
        /// 1，3，2，4一个一个挨着走，
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ComplexWorkFlow3()
        {
            Console.WriteLine("ComplexWorkFlow3 3 ------------");
            // 执行是 挨着 走的
            return await DoTask5(await DoTask3UseResultOfTask1(await DoTask1()), await DoTask4UseResultOfTask2(await DoTask2()));
        }

        /// <summary>
        /// 测试写法
        /// </summary>
        /// <returns></returns>
        public static async Task<int> Test()
        {
            Action af = async () =>
            {
                await Task.Delay(1000);
                //Console.WriteLine(await ComplexWorkFlow());
                //Console.WriteLine(await ComplexWorkFlow2());
                //Console.WriteLine(await ComplexWorkFlow3());
                //Console.WriteLine(await ComplexWorkFlow21());
                //Console.WriteLine(await ComplexWorkFlow22());
            };
            af.TimeOfShell();
            return 1;
        }
    }
}
