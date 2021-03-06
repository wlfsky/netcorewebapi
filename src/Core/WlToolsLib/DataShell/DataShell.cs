﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Extension;

namespace WlToolsLib.DataShell
{

    #region --包装返回类型--

    /// <summary>
    /// 实验功能
    /// 泛型返回数据，用于包装返回数据
    /// 内设置Success是否成功，时间，信息，版本，泛型返回数据
    /// 增加多个成功，和失败设置，统一设置失败和成功的规则
    /// </summary>
    /// <typeparam name="TResult">具体返回类型</typeparam>
    public class DataShell<TResult> : IDataShell<TResult>
    {
        #region --初始化函数组--

        /// <summary>
        /// 初始化 <see cref="{TResult}"/> 类的新实例。
        /// 时间版本自动设置
        /// </summary>
        public DataShell()
        {
            this.Time = DateTime.Now;
            this.Version = "0.1";
            this.Infos = new List<string>();
            this.Code = 0;
            this.Status = 0;
        }

        /// <summary>
        /// 初始化，时间和版本自动设置
        /// </summary>
        /// <param name="successs">是否成功</param>
        /// <param name="r_data">返回数据</param>
        /// <param name="info">信息，无信息可置空</param>
        public DataShell(bool isSuccess, TResult r_data, string info)
            : this()
        {
            this.Success = isSuccess;
            this.Data = r_data;
        }

        /// <summary>
        /// 初始化，时间版本自动设置
        /// </summary>
        /// <param name="successs"></param>
        /// <param name="r_data"></param>
        public DataShell(bool success, TResult r_data)
            : this(success, r_data, string.Empty)
        {
        }

        #endregion --初始化函数组--

        #region --静态初始化--

        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataShell<T> CreateSuccess<T>()
        {
            var t = new DataShell<T>();
            t.Succeed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataShell<T> CreateSuccess<T>(T v)
        {
            var t = new DataShell<T>();
            return t.Succeed(v);
        }

        /// <summary>
        /// 默认初始化一个失败的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataShell<T> CreateFail<T>()
        {
            var t = new DataShell<T>();
            t.Failed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值，并写入 info
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DataShell<T> CreateFail<T>(string info)
        {
            var t = new DataShell<T>();
            t.Failed(info);
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值，并写入 info
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DataShell<T> CreateFail<T>(IList<string> infos)
        {
            var t = new DataShell<T>();
            t.Failed(infos);
            return t;
        }

        #endregion --静态初始化--

        #region --属性组--

        /// <summary>
        /// 获取或设置一个值，该值指示-执行是否成功(true, false)
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 失败，完全和Success相反,只是为了提供方便阅读的属性
        /// </summary>
        public bool Failure { get { return !this.Success; } set { this.Success = !value; } }

        /// <summary>
        /// 获取或设置一个值，该值指示-执行是否成功：0未知，大于0成功true，小于0失败false
        /// (这是个兼容属性)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 代码，当前后端约定代码信息后可使用
        /// 默认 0 表示：无意义
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 消息，提供一个覆盖型单消息
        /// (这是个兼容属性)
        /// </summary>
        public string Msg { get; set; } = "";
        /// <summary>
        /// 获取或设置一个值，该值指示-信息，如果出错返回错误信息
        /// </summary>
        public IList<string> Infos { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-信息，如果出错返回错误详细信息
        /// </summary>
        public IList<string> InfoDetail { get; set; } = new List<string>();

        /// <summary>
        /// 信息,组合了所有Infos的信息
        /// </summary>
        public string Info
        {
            get { return string.Join(",", this.Infos); }
            set
            {
                // 不空切没有此错误信息就加入，重复的不加入
                if (!value.NullEmpty() && !this.Infos.Any((i) => i == value))
                {
                    this.Infos.Add(value);
                }
            }
        }

        /// <summary>
        /// 获取一个值，该值指示-结果时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 获取一个值，该值指示-返回版本 目前默认 0.1
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-返回数据
        /// </summary>
        public TResult Data { get; set; }

        /// <summary>
        /// 异常队，少用
        /// </summary>
        public IList<Exception> ExceptionList { get; set; } = new List<Exception>();

        /// <summary>
        /// 操作者
        /// </summary>
        public IOperator Operator { get; set; }

        #endregion --属性组--

        #region --方法--

        /// <summary>
        /// 统一设置成功业务
        /// </summary>
        public DataShell<TResult> Succeed()
        {
            this.Success = true;
            this.Code = 1;
            this.Status = 1;
            return this;
        }

        /// <summary>
        /// 统一设置成功业务，成功并赋值
        /// </summary>
        /// <param name="data"></param>
        public DataShell<TResult> Succeed(TResult data)
        {
            this.Succeed();
            this.Data = data;
            return this;
        }

        /// <summary>
        /// 统一设置成功业务，成功并赋值
        /// </summary>
        /// <param name="data"></param>
        public DataShell<TResult> Succeed(TResult data, string info)
        {
            this.Succeed();
            this.Data = data;
            this.Infos.Add(info);
            return this;
        }

        /// <summary>
        /// 统一设置失败业务
        /// </summary>
        public DataShell<TResult> Failed()
        {
            this.Success = false;
            this.Code = -1;
            this.Status = -1;
            this.Data = default(TResult);
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入错误信息
        /// </summary>
        /// <param name="errorInfo"></param>
        public DataShell<TResult> Failed(string errorInfo)
        {
            this.Failed();
            this.AddInfo(errorInfo);
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入一堆错误信息
        /// </summary>
        /// <param name="errorInfo"></param>
        public DataShell<TResult> Failed(IList<string> errorInfo)
        {
            this.Failed();
            foreach (var item in errorInfo)
            {
                this.Infos.Add(item);
            }
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入错误信息，只返回基本错误信息
        /// </summary>
        /// <param name="error"></param>
        public DataShell<TResult> Failed(Exception error)
        {
            this.Failed();
            this.Infos.Add(error.Message);
            this.InfoDetail.Add(error.StackTrace);
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入错误信息，可自定义错误信息处理委托
        /// </summary>
        /// <param name="error"></param>
        public DataShell<TResult> Failed(Exception error, Func<Exception, string> errorInfoMaker)
        {
            this.Failed();
            this.Infos.Add(errorInfoMaker(error));
            return this;
        }

        /// <summary>
        /// 设置一个信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public DataShell<TResult> AddInfo(string info)
        {
            if (info.NotNullEmpty())
            {
                this.Infos.Add(info);
            }
            return this;
        }

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public DataShell<TResult> AddInfoDetail(string detail)
        {
            if (detail.NotNullEmpty())
            {
                this.InfoDetail.Add(detail);
            }
            return this;
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        /// <returns></returns>
        public DataShell<TResult> ClearInfo()
        {
            if (this.Infos.HasItem())
            {
                this.Infos.Clear();
            }
            return this;
        }

        /// <summary>
        /// 清空异常
        /// </summary>
        /// <returns></returns>
        public DataShell<TResult> ClearExceptions()
        {
            if (this.ExceptionList.HasItem())
            {
                this.ExceptionList.Clear();
            }
            return this;
        }
        #endregion --方法--
    }

    #endregion --包装返回类型--

    
    
}
