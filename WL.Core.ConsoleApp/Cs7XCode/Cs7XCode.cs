using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace WL.Core.ConsoleApp.Cs7XCode
{
    public class Cs7XCode
    {
        #region --c#7.1新特性测试--
        /*
         * 项目-右键-属性-生成-高级-语言版本：改为最新！
        */

        public static void test_71_to_73_new()
        {
            WriteLine("7.1-7.3新语法测试");
            // ------------------------
            WriteLine("7.1新语法测试");
            default_val();
            InferredTupleElementNames();
            // ------------------------
            WriteLine("7.2新语法测试");
            num_head_under_line();
            // ------------------------
            WriteLine("7.3新语法测试");
            test_enhanced_generic_constraints();
            test_in_overload();
            WriteLine("7.1-7.3新语法测试=========结束 END FIN КОНЕЦ 終わった");
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

        #region --7.2 具有值类型的引用语义--
        /*
         * 这一组新特性主要意义在于快速访问无需修改的堆数据
         * 这有那么一点rust风格，对提升c#性能有很大帮助
         * 一定要去看看dotnet开发团队的 用法
         * 详细阅读以下地址
         * https://docs.microsoft.com/zh-cn/dotnet/csharp/reference-semantics-with-value-types
         */
        /// <summary>
        /// in 注意in是只读
        /// </summary>
        /// <param name="x"></param>
        public static int ref_01(in int x = 1)
        {
            WriteLine("in参数修饰 x = {0}".FormatStr(x));
            return x;
        }
        static int Y = 1234;//这个静态值，可以被ref readonly的函数返回；
        public static ref readonly int ref_return_ref_02() // 注意这里用ref readonly修饰返回值
        {
            int x = 122;
            WriteLine("ref readonly 返回值：{0}".FormatStr(x));
            ref readonly int y = ref x;
            // return ref y;// 如果是ref 的局部变量就不能被 ref readonly return了
            return ref Y; // 外部的就能被ref readonly return
        }

        public class XX
        {
            private static int x1 = 1337;
            public static ref readonly int X1 => ref x1; //如果要构建一个不修改的快速访问就这样很合适
                                                         // 那么疑问：和yield结合使用如何？yield大多都是引用类型而非值类型！
                                                         // 那么一定要去看看 dotnet团队的用法范例
        }

        public static void ref_01_test()
        {
            int x = 1203;
            ref_01(in x);

        }

        #region --readonly struct--
        readonly public struct ReadonlyPoint3D
        {
            public ReadonlyPoint3D(double x, double y, double z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public double X { get; }
            public double Y { get; }
            public double Z { get; }

            private static readonly ReadonlyPoint3D origin = new ReadonlyPoint3D();
            public static ref readonly ReadonlyPoint3D Origin => ref origin;
        }
        #endregion

        #region --ref struct--
        /*
         * 不能对 ref struct 装箱。 无法向属于 object、dynamic 或任何接口类型的变量分配 ref struct 类型。
         * 不能将 ref struct 声明为类或常规结构的成员。
         * 不能声明异步方法中属于 ref struct 类型的本地变量。 不能在返回 Task、Task<T> 或类似 Task 类型的同步方法中声明它们。
         * 无法在迭代器中声明 ref struct 本地变量。
         * 无法捕获 Lambda 表达式或本地函数中的 ref struct 变量。
         */
        /// <summary>
        /// 引用结构体
        /// </summary>
        ref struct ReadOnlyRefPoint2DXY
        {
            public int X { get; }
            public int Y { get; }
            public ReadOnlyRefPoint2DXY(int x, int y) => (X, Y) = (x, y);
        }
        #endregion

        #region --readonly ref struct--
        /// <summary>
        /// 只读引用结构体
        /// </summary>
        readonly ref struct ReadOnlyRefPoint2D
        {
            public int X { get; }
            public int Y { get; }
            public ReadOnlyRefPoint2D(int x, int y) => (X, Y) = (x, y);
        }
        #endregion

        #endregion

        #region --7.2 数值前导下划线--
        /// <summary>
        /// 字面值前导下划线 只有16进制和2进制
        /// </summary>
        public static void num_head_under_line()
        {
            int x = 0x_1f_19_92;
            WriteLine("16进制x ： {0}".FormatStr(x));
            // int o = 0o13_24;
            int b = 0b_1001_1101_1111;
            WriteLine("二进制b ： {0}".FormatStr(b));
        }
        #endregion
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
    }


}
