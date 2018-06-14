using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using reg = System.Text.RegularExpressions;
using static System.Console;
using WlToolsLib.JsonHelper;
using WlToolsLib.i18n;

namespace WL.Core.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ref int GetValue(int[] array, int index) => ref array[index];
            int[] array1 = { 1, 2, 3, 4, 5 };
            ref int x = ref GetValue(array1, 2);
            Console.WriteLine(x);
            x = 4;
            Console.WriteLine(x);
            Console.WriteLine(array1[2]);
            Console.WriteLine("Hello World!");

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

            Console.ReadKey();
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
    }

    #region --临时扩展--
    public static class ex
    {
        #region --全球化支持多语言扩展(解决方案/项目级别扩展)--
        public static string Global(this string self)
        {
            var cl = LanguageFactory.CurrLang(WL.Core.Model.Language.DefLangKV.LangKV);
            if (cl.ContainsKey(self))
            {
                return cl[self];
            }
            cl.Add(self, self);
            return self;
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
