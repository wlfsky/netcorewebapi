using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using reg = System.Text.RegularExpressions;
using static System.Console;
using WlToolsLib.JsonHelper;
using WlToolsLib.i18n;
using System.Threading;
using System.Diagnostics;

namespace WL.Core.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");
            WriteLine("开始测试！");
            ref_func();
            

            //new Program().R();
            //Task.Run(async () => { 
            //    await "dddddd";
            //    Console.WriteLine("ddddd");
            //});
            //Task.Run(async () => {
            //    await new Program().B();
            //    Console.WriteLine("ddddd");
            //});
            //new Program().R();

            #region --测试 enum 和 language--
            WriteLine(SexEnum.Female.EnumToStr());
            #endregion

            #region --生成一个langkvjson--
            var langjson = WL.Core.Model.Language.DefLangKV.LangKV.ToJson();
            WriteLine(langjson);
            #endregion

            #region --打印--
            var gl = "用户名".Global();
            WriteLine(gl);
            #endregion

            #region --路径扩展测试--
            WriteLine(Common.Constant.BasePath);
            var ps = Common.Constant.BasePath.PathSplit();
            foreach (var i in ps)
            {
                WriteLine(i);
            }
            var nps = Common.Constant.BasePath.PathReturn(2);
            WriteLine(nps);
            #endregion

            #region --加入语言项测试--
            var g1 = "验证码".Global();
            WriteLine(g1);
            #endregion


            #region --DateTime.Now.Ticks 生成测试--
            var fileTicks = System.IO.File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "TicksMakeTest.txt");
            for (int i = 0; i < 200; i++)
            {
                fileTicks.WriteLine(i.ToString() + "==" + DateTime.Now.Ticks.ToString());
                Thread.Sleep(1);
            }
            fileTicks.Close();
            #endregion


            #region --处理集群测试--
            //test_the_cluster();
            #endregion

            #region --yield 简化 连续的 是真时能转换 委托====测试--
            var x1 = new List<int>() { 1, 2, 3, 4 };
            foreach (var item in new Program().TrueCanTrans(x1, z=>z>1, z=>z+1))
            {
                Console.WriteLine(item.ToString());
            }
            #endregion

            #region --测试7.1-7.3新语法--
            test_71_73_new();
            #endregion

            ReadKey();
        }

        #region --c#7.1新特性测试--
        /*
         * 项目-右键-属性-生成-高级-语言版本：改为最新！
        */

        public static void test_71_73_new()
        {
            WriteLine("7.1-7.3新语法测试");
            WriteLine("7.1新语法测试");
            default_val();
            InferredTupleElementNames();
            WriteLine("7.2新语法测试");

            WriteLine("7.3新语法测试");
            test_enhanced_generic_constraints();
            test_in_overload();
        }

        #region --7.1新语法测试--
        /// <summary>
        /// 7.1才支持的一部main方法，编译失败
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main()
        {
            await Task.Run(() =>
            {
                WriteLine("start");
                var l = new[] { 6, 7, 8, 9, 10 };
                foreach (var i in l)
                {
                    WriteLine("{0}......".FormatStr(i));
                    System.Threading.Thread.Sleep(100);
                }
                WriteLine("stop");
            });
            ReadKey();
        }

        /// <summary>
        /// 默认值，default 编译失败，其他不测试了肯定没有到7.1
        /// 感觉要叫 推断默认值比较好
        /// </summary>
        public static void default_val()
        {
            int i = default;
            WriteLine("default 推断默认值测试（int）：{0}".FormatStr(i));
            Func<int, string> fu = default;
            fu = x => x.ToString();
            var r = fu(123);
            WriteLine("default 推断默认值测试（委托）：{0}".FormatStr(r));
        }

        /// <summary>
        /// 推断元组元素名 编译不通过，但是提示错误含有对7.1的指引。表示ide认识此语法
        /// </summary>
        public static void InferredTupleElementNames()
        {
            int count = 5;
            string label = "Colors used in the map";
            var pair = (count, label);
            WriteLine("元组推断元组变量名称：{0}".FormatStr(pair.count));
        }
        #endregion

        #region --7.2新语法测试--

        #endregion

        #region --7.3新语法测试--
        #region --泛型约束支持enum 和 delegate--
        public enum XTYPE
        {
            XT01 = 1,
            XT02 = 2
        }

        public static string ToStr<T>(T t) where T : System.Enum
        {
            var r = t.ToString();
            WriteLine("委托枚举约束结果：{0}".FormatStr(r));
            return r;
        }

        public delegate string FN(int i);
        public static FN f = i => i.ToString();

        public static string ToRun<F>(F f, int i) where F : System.Delegate
        {
            var r = f.DynamicInvoke(i).ToString();
            WriteLine("委托泛型约束结果：{0}".FormatStr(r));
            return r;
        }

        public static void test_enhanced_generic_constraints()
        {
            ToStr<XTYPE>(XTYPE.XT01);
            ToRun<FN>(f, 5);
        }
        #endregion

        #region --修复in 重载问题--

        public static void test_in_overload()
        {
            WriteLine("in参数方法重载修复测试");
            var r1 = M(1923);
            int ref_num = 234_32;
            var r2 = M(in ref_num);
        }

        public static string M(int i)
        {
            var r = i.ToString();
            WriteLine("这是没有in的方法M：{0}".FormatStr(r));
            return r;
        }

        public static string M(in int i)
        {
            var r = i.ToString();
            WriteLine("这是有in的方法in M：{0}".FormatStr(r));
            return r;
        }
        #endregion
        #endregion

        #endregion

        /// <summary>
        /// 引用局部变量
        /// </summary>
        public static void ref_func()
        {
            ref int GetValue(int[] array, int index) => ref array[index];
            int[] array1 = { 1, 2, 3, 4, 5 };
            ref int x = ref GetValue(array1, 2);
            Console.WriteLine(x);
            x = 4;
            Console.WriteLine(x);
            Console.WriteLine(array1[2]);
        }

        public void Check()
        {
            var b = new Book();
            var c = new Dictionary<string, Func<bool>>() {
                ["无数据"] = () => b.IsNull(),
                ["无id"] = () => b.BookID <= 0,
                ["无价格"] = () => b.BookPrice <= 0m
            };
            var err = c.SimpleChecker();
            if (err.NotNullEmpty())
            {
                // return error
            }
            // ----
            (bool success, string info) = c.Checker();
            if (success == false)
            {
                // return info;
            }
            
            var d = b.GetType().GetProperties().First().PropertyType.Name;
        }

        public static void TimeOf_Shell(Action xn = null)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (xn.NotNull())
            {
                xn();
            }
            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed);
            Console.WriteLine("In milliseconds: {0}", watch.ElapsedMilliseconds);
            Console.WriteLine("In timer ticks: {0}", watch.ElapsedTicks);
        }


        static (string name, string site) Foo()
        {
            return (name: "lindexi", site: "blog.csdn.net/lindexi_gd");
        }

        public static int t()
        {
            if (int.TryParse("123", out int value))
            {
                return value;
            }
            else
            {
                return 3;
            }
        }

        #region --简单协程--
        public async Task B()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("start");
                var l = new[] { 1, 2, 3, 4, 5 };
                foreach (var i in l)
                {
                    Console.WriteLine("{0}......".FormatStr(i));
                    System.Threading.Thread.Sleep(1000);
                }
                Console.WriteLine("stop");
            });
        }
        public async Task P()
        {
            await B();
            await B();
        }

        public void R()
        {
            Task.Run(async () => { await B(); await B(); });
        }
        #endregion

        #region --测试写法--

        /// <summary>
        /// yield 简化 连续的 是真时能转换 委托====测试
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="sou"></param>
        /// <param name="checker"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public IEnumerable<TOut> TrueCanTrans<TIn, TOut>(IEnumerable<TIn> sou, Func<TIn, bool> checker, Func<TIn, TOut> trans)
        {
            var result = new List<TOut>();
            foreach (var item in YieldEachFunc(YieldEachPredicate(sou, checker), trans))
            {
                result.Add(item);
                //yield return item;
            }
            return result;
        }


        public IEnumerable<T> YieldEachPredicate<T>(IEnumerable<T> sou, Func<T, bool> predicate)
        {
            foreach (var item in sou)
            {
                if (predicate(item))
                {
                    Console.WriteLine("predicate");
                    yield return item;
                }
            }
        }
        public IEnumerable<TOut> YieldEachFunc<TIn, TOut>(IEnumerable<TIn> sou, Func<TIn, TOut> func)
        {
            foreach (var item in sou)
            {
                Console.WriteLine("func");
                yield return func(item);
            }
        }
        #endregion

        #region --集群分体处理测试方法入口--
        public static void test_the_cluster()
        {
            var fsio00 = System.IO.File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "log_00.txt");

            TaskWork<int, string> tw = new TaskWork<int, string>();
            tw.PCluster.ProcessHandle = (i) =>
            {
                //Task.Delay(2000);
                Thread.Sleep(500);
                var str = "===={0}====".FormatStr(i.TaskPID);

                //fsio00.WriteLine(str);
                return new TaskID<string>() { TaskPID = i.TaskPID, TaskData = str };
            };

            tw.OutputTask.PullProcess = (p) =>
            {
                //Task.Delay(200);
                Thread.Sleep(100);
                fsio00.WriteLine("===> Saved {0}.".FormatStr(p.TaskPID));
            };

            tw.RunTask();
            tw.OutputStatus();
            WriteLine("==start push==");
            Thread t = new Thread(new ThreadStart(() =>
            {
                for (int i = 100; i < 200; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            }));
            t.Start();
            Thread t2 = new Thread(new ThreadStart(() =>
            {
                for (int i = 200; i < 400; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            }));
            t2.Start();
            System.Threading.Tasks.Parallel.Invoke(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            }, () =>
            {
                for (int i = 100; i < 200; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            });
            for (int i = 0; i < 100; i++)
            {
                if (tw.OutputTask.DiverterCount > 0)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
            //Thread.Sleep(20000);
            //Task.Delay(10000);
            tw.PCluster.Stop();
            fsio00.Close();
        }
        #endregion
    }

    #region --临时扩展--
    public static class ex
    {
        #region --全球化支持多语言扩展(解决方案/项目级别扩展)--
        public static string Global(this string self)
        {
            var val = LanguageFactory.Global(self, WL.Core.Model.Language.DefLangKV.LangKV);
            return val;
        }
        #endregion
    }
    #endregion

    #region --扩展string异步--
    public static class KvpbamjhKsvm
    {
        public static HeabdsdnbKevx GetAwaiter(this string str)
        {
            return new HeabdsdnbKevx();
        }
    }

    public class HeabdsdnbKevx : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult()
        {
        }

        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }
    #endregion
    /// <summary>
    /// 错误检查结果
    /// </summary>
    public class CheckResult
    {
        public bool Success { get; set; }
        public string Info { get; set; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public void Deconstruct(out bool success, out string info) { success = Success; info = Info; }
    }
    /// <summary>
    /// 错误检查结果泛型版
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CheckResult<TData> : CheckResult
    {
        public TData Data { get; set; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public void Deconstruct(out bool success, out string info, out TData data) { success = Success; info = Info; data = Data; }
    }

    public class Book
    {
        public int BookID { get; set; }
        public decimal BookPrice { get; set; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public void Deconstruct(out int id, out decimal price) { id = BookID; price = BookPrice; }
    }
}
