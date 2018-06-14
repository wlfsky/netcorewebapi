using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WL.Core.ConsoleApp
{
    public static class ExpandMothed
    {
        #region --枚举扩展--
        /// <summary>
        /// 枚举值翻译文字
        /// </summary>
        /// <param name="self"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string EnumToStr(this Enum self, int enumValue)
        {
            var enumType = self.GetType();
            var result = enumType.GetEnumName(enumValue) ?? "无值";
            return result;//这里变量转一下只是为了方便debug
        }
        #endregion

        #region --可枚举扩展--
        /// <summary>
        /// 移除符合筛选断言的元素
        /// 注意所有元素拷贝到了另一个IEnumerable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="self">可枚举结构</param>
        /// <param name="predicate">筛选断言</param>
        /// <returns></returns>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            var result = new List<T>();
            foreach (var item in self)
            {
                if (predicate(item))
                {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 创建一个迭代器带有索引的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, T>> ForeachIndex<T>(this IEnumerable<T> source)
        {
            if (source.NotNull())
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    var souTemp = source.ElementAt(i);
                    yield return new Tuple<int, T>(i, souTemp);
                }
            }
        }

        /// <summary>
        /// 用指定的字符串拼接由制定转换器转换出来的字符串组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="converter"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Junctor<T>(this IEnumerable<T> self, Func<T, string> converter, string separator = ",")
        {
            if (converter.IsNull() || self.NoItem())
            {
                return string.Empty;
            }
            List<string> t = new List<string>();
            foreach (var i in self)
            {
                t.Add(converter(i));
            }
            string r = string.Join(separator, t);
            return r;
        }


        /// <summary>
        /// 枚举中是否有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasItem<T>(this IEnumerable<T> self)
        {
            if (self.IsNull())
            {
                return false;
            }
            if (self.Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 枚举中没有项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NoItem<T>(this IEnumerable<T> self)
        {
            return !self.HasItem();
        }

        #endregion

        #region --string 扩展--
        /// <summary>
        /// 字符串是否空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="includeWhiteSpace">是否包含空格，默认true</param>
        /// <returns></returns>
        public static bool NullEmpty(this string self, bool includeWhiteSpace = true)
        {
            if (includeWhiteSpace)
            {
                return string.IsNullOrWhiteSpace(self);
            }
            else
            {
                return string.IsNullOrEmpty(self);
            }
        }

        /// <summary>
        /// 是否不为空或者空字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNullEmpty(this string self)
        {
            return !self.NullEmpty();
        }

        /// <summary>
        /// 去除两端空格
        /// null不报错，自动设置为string.Empty
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string PowerTrim(this string self)
        {
            if (self.NullEmpty())
            {
                return string.Empty;
            }
            else
            {
                return self.Trim();
            }
        }

        /// <summary>
        /// string.Format简化版
        /// </summary>
        /// <param name="self"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatStr(this string self, params object[] args)
        {
            // 无模板字符串返回空
            if (self.NullEmpty() || self.IndexOf("{0}") < 0)
                return string.Empty;
            return string.Format(self, args);
        }

        /// <summary>
        /// 检查字符串长度
        /// </summary>
        /// <param name="self"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool LenCheck(this string self, int min, int? max = null)
        {
            if (max.HasValue == false)
            {
                max = min;
            }
            if (min < 0 || max < 0 || min > max)//小值小于0，大值小于0，小值大于大值均返回false
            {
                return false;
            }
            if (self.NullEmpty())//检查字符串为空或0长度 且小值等于0时返回true，否则返回false
            {
                return min == 0 ? true : false;
            }
            var len = self.Length;
            if (len < min || len > max)
            {
                return false;
            }
            else if (len >= min && len <= max)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region --检查对象是否null--
        /// <summary>
        /// 检查对象是否null，是返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T self)
        {
            return ReferenceEquals(self, null);
            //return self is null;//此句与泛型不匹配
        }

        /// <summary>
        /// 检查对象是否null 不是返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNull<T>(this T self)
        {
            return !self.IsNull();
        }
        #endregion --检查对象是否null--

        #region --数值类型 扩展--
        /// <summary>
        /// 自定义限制数据范围
        /// 自定义限制对比断言
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">可比大小的，且有范围的类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <param name="max_predicate">大比断言</param>
        /// <param name="min_predicate">小比断言</param>
        /// <returns></returns>
        public static T Limit<T>(this T self, T max, T min, Func<T, T, bool> max_predicate, Func<T, T, bool> min_predicate)
        {
            if (max_predicate(self, max))
            {
                self = max;
            }
            if (min_predicate(self, min))
            {
                self = min;
            }
            return self;
        }

        /// <summary>
        /// int类型限制数据范围
        /// 自动约束到范围内，超最大设置成最大，超最小设置成最小，其间不处理
        /// </summary>
        /// <param name="self">int类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static int Limit(this int self, int max, int min)
        {
            return self.Limit<int>(max, min, (m, mx) => m > mx, (m, mn) => m < mn);
        }

        /// <summary>
        /// long类型限制数据范围
        /// 自动约束到范围内，超最大设置成最大，超最小设置成最小，其间不处理
        /// </summary>
        /// <param name="self">long类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static long Limit(this long self, long max, long min)
        {
            return self.Limit<long>(max, min, (m, mx) => m > mx, (m, mn) => m < mn);
        }

        /// <summary>
        /// float类型限制数据范围
        /// 自动约束到范围内，超最大设置成最大，超最小设置成最小，其间不处理
        /// </summary>
        /// <param name="self">float类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static float Limit(this float self, float max, float min)
        {
            return self.Limit<float>(max, min, (m, mx) => m > mx, (m, mn) => m < mn);
        }
        #endregion

        #region --时间扩展--
        /// <summary>
        /// 获取指定时间次日0点 00:00:00.000
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime NextDay0Hour(this DateTime self)
        {
            var t_datetime = self.AddDays(1);
            t_datetime = t_datetime - t_datetime.TimeOfDay;
            return t_datetime;
        }
        /// <summary>
        /// 当日0点 00:00:00.000
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime CurrDay0Hour(this DateTime self)
        {
            var t_datetime = self;
            t_datetime = t_datetime - t_datetime.TimeOfDay;
            return t_datetime;
        }

        /// <summary>
        /// 当天最后一秒 23:59:59.999
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime CurrDayLastSecond(this DateTime self)
        {
            var t_datetime = new DateTime(self.Year, self.Month, self.Day, 23, 59, 59, 999);
            return t_datetime;
        }

        /// <summary>
        /// 返回日期时间完整字符串 yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string FullStr(this DateTime self, string dateIntervalChar = "-", string timeIntervalChar = ":", string msIntervalChar = ".")
        {
#if VS2017
            var shortDateStr = self.ToString($"yyyy{dateIntervalChar}MM{dateIntervalChar}dd HH{timeIntervalChar}mm{timeIntervalChar}ss{msIntervalChar}fff");
#else
            var shortDateStr = self.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss{2}fff", dateIntervalChar, timeIntervalChar, msIntervalChar));
#endif
            return shortDateStr;
        }

        /// <summary>
        /// 返回日期时间完整字符串，无毫秒 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dateIntervalChar"></param>
        /// <param name="timeIntervalChar"></param>
        /// <returns></returns>
        public static string DateTimeStr(this DateTime self, string dateIntervalChar = "-", string timeIntervalChar = ":")
        {
#if VS2017
            var shortDateStr = self.ToString($"yyyy{dateIntervalChar}MM{dateIntervalChar}dd HH{timeIntervalChar}mm{timeIntervalChar}ss");
#else
            var shortDateStr = self.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", dateIntervalChar, timeIntervalChar));
#endif
            return shortDateStr;
        }

        /// <summary>
        /// 返回时间的日期字符串 yyyy-MM-dd
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dateIntervalChar"></param>
        /// <returns></returns>
        public static string DateStr(this DateTime self, string dateIntervalChar = "-")
        {
#if VS2017
            var shortDateStr = self.ToString($"yyyy{dateIntervalChar}MM{dateIntervalChar}dd");
#else
            var shortDateStr = self.ToString(string.Format("yyyy{0}MM{0}dd", dateIntervalChar));
#endif
            return shortDateStr;
        }
        #endregion

        #region --拷贝扩展--
        /// <summary>
        /// 传送拷贝
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="func_copy"></param>
        /// <returns></returns>
        public static TT TransCopy<TS, TT>(this TS self, Func<TS, TT> func_copy)
        {
            return func_copy(self);
        }

        /// <summary>
        /// 用单个转换直接转换列表数据
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="sou"></param>
        /// <returns></returns>
        public static IList<TT> TransList<TS, TT>(this Func<TS, TT> self, IList<TS> sou)
        {
            var rl = new List<TT>();
            foreach (var si in sou)
            {
                rl.Add(self(si));
            }
            return rl;
        }

        /// <summary>
        /// 将单个转换委托，转换成列表转换委托
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Func<IList<TS>, IList<TT>> TransIListFunc<TS, TT>(this Func<TS, TT> self)
        {
            Func<IList<TS>, IList<TT>> tl = sl =>
            {
                var rl = new List<TT>();
                foreach (var si in sl)
                {
                    rl.Add(self(si));
                }
                return rl;
            };
            return tl;
        }

        /// <summary>
        /// 将单个转换委托，转换成列表转换委托
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="looprefunc">循环中再处理</param>
        /// <returns></returns>
        public static Func<List<TS>, List<TT>> TransListFunc<TS, TT>(this Func<TS, TT> self, Func<TT, TT> looprefunc = null)
        {
            Func<List<TS>, List<TT>> tl = sl =>
            {
                var rl = new List<TT>();
                foreach (var si in sl)
                {
                    var t1 = self(si);
                    if (looprefunc.NotNull()) { t1 = looprefunc(t1); }
                    rl.Add(t1);
                }
                return rl;
            };
            return tl;
        }

        #endregion

        #region --正则表达式扩展--
        /// <summary>
        /// 正则是否匹配，匹配返回true
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool RegexIsMatch(this string pattern, string content)
        {
            if (pattern.NotNullEmpty())
            {
                var r = new Regex(pattern);
                if (r.IsMatch(content))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 不匹配，返回true
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool RegexUnMatch(this string pattern, string content)
        {
            return !pattern.RegexIsMatch(content);
        }

        /// <summary>
        /// 正则匹配所有项
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<Match> RegexMatch(this string pattern, string content)
        {
            List<Match> matchList = null;
            if (pattern.NotNullEmpty())
            {
                var r = new Regex(pattern);
                var matchs = r.Matches(content);
                matchList = new List<Match>();
                if (matchs.Count > 0)
                {
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        matchList.Add(matchs[i]);
                    }
                }

            }
            return null;
        }
        #endregion

        #region --接口转换实例--
        /// <summary>
        /// 接口转换实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> TransInstance<T>(this IList<T> self)
        {
            if (self is List<T>)
            {
                return self as List<T>;
            }
            else
            {
                var t = new List<T>();
                t.AddRange(self.ToList());
                return t;
            }
        }
        #endregion

        #region --数据对比--
        /// <summary>
        /// 自定义对比去重复
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="property"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TSource, bool> property, Func<TSource, TKey> expr)
        {
            EqualityComparer<TSource, TKey> comparer = new EqualityComparer<TSource, TKey>(property, expr);
            return items.Distinct(comparer);
        }

        #region --定义对比--
        /// <summary>
        /// 自定义对比
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        public class EqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
        {
            /// <summary>
            /// 初始化自定义对比
            /// </summary>
            /// <param name="comparer"></param>
            /// <param name="keyselecter"></param>
            public EqualityComparer(Func<TSource, TSource, bool> comparer, Func<TSource, TKey> keyselecter)
            {
                _comparer = comparer;
                _keyselecter = keyselecter;
            }
            /// <summary>
            /// 主键
            /// </summary>
            Func<TSource, TKey> _keyselecter = null;
            /// <summary>
            /// 对比器
            /// </summary>
            Func<TSource, TSource, bool> _comparer = null;
            /// <summary>
            /// 对比接口实现
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(TSource x, TSource y)
            {
                if (_comparer == null)
                {
                    _comparer = (m, n) => { return _keyselecter(m).GetHashCode().Equals(_keyselecter(n).GetHashCode()); };
                }
                return _comparer(x, y);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int GetHashCode(TSource obj)
            {
                return _keyselecter(obj).GetHashCode();
            }
        }
        #endregion
        #endregion

        #region --异常扩展--
        /// <summary>
        /// 返回异常调试信息全量
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string DebugInfo(this Exception self)
        {
            return "{{\"Message\":\"{0}\",\"Source\":\"{1}\",\"StackTrace\":\"{2}\"}}".FormatStr(self.Message, self.Source, self.StackTrace);
        }
        #endregion

        #region --错误检查扩展--
        /// <summary>
        /// 错误检查简化版，有错误返回错误信息，无错误返回空字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string SimpleChecker(this Dictionary<string, Func<bool>> self)
        {
            foreach (var key in self.Keys)
            {
                if (self[key].NotNull() && self[key]())
                {
                    return key;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 错误检查：返回值加强版。
        /// 有错误返回是否有错（注意是返回是否有错 true有错，false无错）
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static (bool success, string info) Checker(this Dictionary<string, Func<bool>> self)
        {
            foreach (var eKey in self.Keys)
            {
                if (self[eKey].NotNull() && self[eKey]())
                {
                    return (true, eKey);
                }
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// 错误检查常规版，有错误返回是否有错（注意是返回是否有错 true有错，false无错）
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static (bool success, string info) Checker<TData>(this Dictionary<string, Func<TData, bool>> self, TData data)
        {
            foreach (var eKey in self.Keys)
            {
                if (self[eKey].NotNull() && self[eKey](data))
                {
                    return (true, eKey);
                }
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// 对列表的错误检查
        /// 任何一个数据出现错误
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="self"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static (bool success, string info, TData data) CheckerList<TData>(this Dictionary<string, Func<TData, bool>> self, IEnumerable<TData> dataList)
        {
            if (dataList.HasItem())
            {
                foreach (var item in dataList)
                {
                    if (self.NotNull() && self.Keys.HasItem())
                    {
                        foreach (var eKey in self.Keys)
                        {
                            if (self[eKey].NotNull() && self[eKey](item))
                            {
                                return (true, eKey, item);
                            }
                        }
                    }
                }
            }
            return (false, string.Empty, default(TData));
        }
        #endregion

        #region --可空时间类型扩展--
        /// <summary>
        /// 可空时间类型 日期时间字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string DateTimeStr(this DateTime? self)
        {
            if (self.HasValue)
            {
                return self.Value.DateTimeStr();
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
