using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace WlToolsLib.Extension
{
    public static class StringExpand
    {
        #region --简化验证--

        /// <summary>
        /// string.IsNullOrWiteSpace的简化版
        /// 支持检测null的字符类型（好tm强大）
        /// </summary>
        /// <param name="self">扩展字符串</param>
        /// <returns></returns>
        public static bool NullEmpty(this string self) => string.IsNullOrWhiteSpace(self);

        /// <summary>
        /// string.IsNullOrWiteSpace的简化版
        /// 支持检测null的字符类型（好tm强大）
        /// </summary>
        /// <param name="self">扩展字符串</param>
        /// <returns></returns>
        public static bool NotNullEmpty(this string self) => !self.NullEmpty();

        /// <summary>
        /// null或者空字符串返回true，空格和有值false
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NullStr(this string self) => string.IsNullOrEmpty(self);

        /// <summary>
        /// 不为null或者空字符串返回true，null或者空字符串返回false
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNullStr(this string self) => !self.NullStr();

        #endregion --简化验证--

        /// <summary>
        /// 从字符串左边开始查找，返回两个字符串 之间的字符串，去两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <returns></returns>
        public static string SinceStr(this string self, string leftStr, string rightStr)
        {
            if (self.NullEmpty()) return string.Empty;
            if (leftStr.NullStr() && rightStr.NullStr()) return self;
            var s = self.IndexOf(leftStr);
            var e = self.IndexOf(rightStr);
            if (e <= s) return string.Empty;
            if (s < 0 && e < 0) return self;
            if (s < 0) s = 0;
            if (e < 0) e = self.Length;
            var leftLen = leftStr.Length;
            var l = e - s - leftLen;
            if (l <= 0) return string.Empty;
            var sinceStr = self.Substring(s + leftLen, l);
            if (sinceStr.NullEmpty()) return string.Empty;
            return sinceStr.Trim();
        }

        /// <summary>
        /// 从字符串右边开始查找，返回两个字符串 之间的字符串，去两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <returns></returns>
        public static string RightSinceStr(this string self, string leftStr, string rightStr)
        {
            if (self.NullEmpty()) return string.Empty;
            if (leftStr.NullStr() && rightStr.NullStr()) return self;
            var s = self.LastIndexOf(leftStr);
            var e = self.LastIndexOf(rightStr);
            if (e <= s) return string.Empty;
            if (s < 0 && e < 0) return self;
            if (s < 0) s = 0;
            if (e < 0) e = self.Length;
            var leftLen = leftStr.Length;
            var l = e - s - leftLen;
            if (l <= 0) return string.Empty;
            var sinceStr = self.Substring(s + leftLen, l);
            if (sinceStr.NullEmpty()) return string.Empty;
            return sinceStr.Trim();
        }

        /// <summary>
        /// 从左边搜索左字符串，右边搜索右字符串，返回两个字符串之间的内容
        /// </summary>
        /// <param name="self"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <returns></returns>
        public static string BetweenSinceStr(this string self, string leftStr, string rightStr)
        {
            if (self.NullEmpty()) return string.Empty;
            if (leftStr.NullStr() && rightStr.NullStr()) return self;
            var len = self.Length;
            var s = self.IndexOf(leftStr);
            var e = self.LastIndexOf(rightStr);
            if (e <= s) return string.Empty;
            if (s < 0 && e < 0) return self;
            if (s < 0) s = 0;
            if (e < 0) e = len;
            var leftLen = leftStr.Length;
            var l = e - s - leftLen;
            if (l <= 0) return string.Empty;
            var sinceStr = self.Substring(s + leftLen, l);
            if (sinceStr.NullEmpty()) return string.Empty;
            return sinceStr.Trim();
        }

        /// <summary>
        /// 仿python切片,string版本
        /// </summary>
        /// <param name="self"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Slice(this string self, int? s = null, int? e = null, int? step = null)
        {
            /*两边都没有返回原值*/
            if (self.IsNull()) return self;
            if (!s.HasValue && !e.HasValue) return self;
            if (!s.HasValue && e.HasValue) { return self.Substring(0, e.Value); }
            if (s.HasValue && !e.HasValue) { return self.Substring(s.Value, self.Length - s.Value); }
            return self;
        }

        #region --去除两端空格--

        /// <summary>
        /// 去两端空格，如果无值或者null用Empty返回，不会报异常
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string PowerTrim(this string self)
        {
            if (self.NotNullEmpty())
            {
                self = self.Trim();
                return self;
            }
            return string.Empty;
        }

        /// <summary>
        /// 实体的常用字符类型字段整合处理，空或空字符串用默认值替代再去掉两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static string TrimOrDef(this string self, string defVal = "")
        {
            if (self.NullEmpty())
            {
                self = defVal;
            }
            return self.PowerTrim();

        }

        /// <summary>
        /// 实体的常用字符类型字段整合处理
        /// 空或空字符串用默认值(string.Empty)替代再去掉两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string TrimOrDef(this string self)
        {
            return self.TrimOrDef(string.Empty);
        }

        /// <summary>
        /// 去两端空格，对象为单位，还无法递归
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void ObjStrTrim<T>(this T self, bool filterSwitch = true, List<string> filterList = null) where T : class
        {
            if (self.NotNull())
            {
                var strType = typeof(string);
                var objType = self.GetType();
                foreach (var properItem in objType.GetProperties())
                {
                    if (properItem.PropertyType == strType)
                    {
                        var v = Convert.ToString(properItem.GetValue(self));
                        if (v.NotNullEmpty())
                        {
                            v = v.Trim();
                        }
                        properItem.SetValue(self, v);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 根据路径获取txt文件字符串，目前只能utf-8编码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetTextFile(this string path)
        {
            if (path.NotNullEmpty())
            {
                using (var fs = System.IO.File.OpenText(path))
                {
                    var jsonStr = fs.ReadToEnd();
                    return jsonStr;
                }
            }
            return null;
        }

        

        #region --两边起点查找，提取特定字符串左右两侧的字符串--
        /// <summary>
        /// 从右侧开始查找positionStr，提取字符串positionStr右侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string LastIndexOfRight(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return self; }
            var position = self.LastIndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            position += 1;
            var lastStr = self.Substring(position, self.Length - position);
            return lastStr;
        }

        /// <summary>
        /// 从结尾开始查找positionStr，提取字符串positionStr左侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string LastIndexOfLeft(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return self; }
            var position = self.LastIndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(0, position);
            return lastStr;
        }

        /// <summary>
        /// 从左则开始查找positionStr，提取字符串positionStr左侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string IndexOfLeft(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return self; }
            var position = self.IndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(0, position);
            return lastStr;
        }

        /// <summary>
        /// 从左则开始查找positionStr，提取字符串positionStr右侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string IndexOfRight(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return self; }
            var position = self.IndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            position += 1;
            var lastStr = self.Substring(position, self.Length - position);
            return lastStr;
        }
        #endregion

        #region --组合字符串--
        /// <summary>
        /// string的join的简化写法
        /// </summary>
        /// <param name="self"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinBy(this IEnumerable<string> self, string separator = ",")
        {
            if (self.IsNull())
            {
                return null;
            }
            if (self.HasItem().IsFalse())
            {
                return "";
            }
            return string.Join(separator, self);
        }
        #endregion

        #region --编辑字符串(首字母大小写转换)--
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string HeadUpper(this string self)
        {
            if (self.NullEmpty())
            {
                return self;
            }
            return self[0].ToString().ToUpper() + self.Substring(1);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string HeadLower(this string self)
        {
            if (self.NullEmpty())
            {
                return self;
            }
            return self[0].ToString().ToLower() + self.Substring(1);
        }
        #endregion

        #region --去掉字符串的文件名非法字符--

        /// <summary>
        /// 去掉字符串的文件名非法字符
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string RemoveFileNameIllegalChar(this string self)
        {
            // \ / : * ? "< > |
            return self.Replace("\\", "")
                .Replace("/", "")
                .Replace(":", "")
                .Replace("*", "")
                .Replace("?", "")
                .Replace("\"", "")
                .Replace("\'", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");

        }
        #endregion

        #region --string.Format 简化--
        /// <summary>
        /// string.Format 简化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatStr(this string self, params object[] args)
        {
            return string.Format(self, args);
        }
        #endregion

        #region --检查字符串长度--
        /// <summary>
        /// 检查字符串长度
        /// 在指定范围内返回true，其他任何异常都返回false
        /// 不提供最大值，就是必须等于最小值
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

        #region --字符串转换utf-8的 byte--
        /// <summary>
        /// 字符串转编码
        /// </summary>
        /// <param name="self"></param>
        /// <param name="code">默认null，是null就用utf8处理</param>
        /// <returns></returns>
        public static byte[] ToByte(this string self, Encoding code = null)
        {
            if (self.NullEmpty())
            {
                return default(byte[]);
            }
            if (code == null)
            {
                code = Encoding.UTF8;
            }
            var t = code.GetBytes(self);
            return t;
        }
        #endregion

        #region --字符串转换utf-8的 ReadOnlyMemory<byte>--
        /// <summary>
        /// 字符串转编码
        /// </summary>
        /// <param name="self"></param>
        /// <param name="code">默认null，是null就用utf8处理</param>
        /// <returns></returns>
        public static ReadOnlyMemory<byte> ToReadOnlyMemoryByte(this string self, Encoding code = null)
        {
            if (self.NullEmpty())
            {
                return null;
            }
            if (code == null)
            {
                code = Encoding.UTF8;
            }
            var t = new ReadOnlyMemory<byte>(code.GetBytes(self));
            return t;
        }
        #endregion

        #region --编码转换类方法--
        /// <summary>
        /// 字符串转换Url编码
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToUrlEncode(this string self)
        {
            if (self.NullEmpty())
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(self); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 转换base64编码字符串
        /// </summary>
        /// <param name="self"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToBase64Str(this string self, Encoding encoding = null)
        {
            if (self.NullEmpty())
            {
                return string.Empty;
            }
            if (encoding.IsNull())
            {
                encoding = Encoding.UTF8;
            }
            byte[] byteStr = encoding.GetBytes(self);
            var res = Convert.ToBase64String(byteStr);
            return res;
        }
        #endregion

        #region --默认断言，专属字符串的默认断言方法--
        /// <summary>
        /// 大小写转相同断言
        /// </summary>
        /// <param name="self">方法承载</param>
        /// <param name="otherStr">另一个字符串</param>
        /// <returns></returns>
        public static bool SameCasePredicate(this string self, string otherStr)
        {
            return self.ToLower() == otherStr.ToLower();
        }
        #endregion

        #region --字符串替换--
        /// <summary>
        /// 用新字符串替换指定位置的内容
        /// </summary>
        /// <param name="self"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="newstr"></param>
        /// <returns></returns>
        public static string Replace(this string self, int start, int end = 0, string newstr = "")
        {
            if (self.NullEmpty()) { return self; }
            var len = self.Length;
            if (end <= 0) { end = len - 1; }
            if (end > len - 1) { end = len - 1; }
            if (start < 0) { start = 0; }
            if (start >= len - 1) { return self; }
            if (start >= end) { return newstr; }
            string head = self.Substring(0, start);
            string btm = self.Substring(end, len - 1 - end);
            return $"{head}{newstr}{btm}";
        }
        #endregion
    }
}
