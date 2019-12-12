using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.DataShell
{
    public interface IDataShell<TResult>
    {
        /// <summary>
        /// 获取或设置一个值，该值指示-执行是否成功(true, false)
        /// </summary>
        bool Success { get; set; }
        /// <summary>
        /// 失败，完全和Success相反,只是为了提供方便阅读的属性
        /// </summary>
        bool Failure { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-执行是否成功：0未知，大于0成功true，小于0失败false
        /// (这是个兼容属性)
        /// </summary>
        int Status { get; set; }
        /// <summary>
        /// 代码，当前后端约定代码信息后可使用
        /// 默认 0 表示：无意义
        /// </summary>
        int Code { get; set; }
        /// <summary>
        /// 消息，提供一个覆盖型单消息
        /// (这是个兼容属性)
        /// </summary>
        string Msg { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示-信息，如果出错返回错误信息
        /// </summary>
        IList<string> Infos { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-信息，如果出错返回错误详细信息
        /// </summary>
        IList<string> InfoDetail { get; set; }
        /// <summary>
        /// 信息,组合了所有Infos的信息
        /// </summary>
        string Info { get; set; }
        /// <summary>
        /// 获取一个值，该值指示-结果时间
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>
        /// 获取一个值，该值指示-返回版本 目前默认 0.1
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-返回数据
        /// </summary>
        TResult Data { get; set; }

        /// <summary>
        /// 异常队，少用
        /// </summary>
        IList<Exception> ExceptionList { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        IOperator Operator { get; set; }

        /// <summary>
        /// 统一设置成功业务
        /// </summary>
        DataShell<TResult> Succeed();

        /// <summary>
        /// 统一设置成功业务，成功并赋值
        /// </summary>
        /// <param name="data"></param>
        DataShell<TResult> Succeed(TResult data);

        /// <summary>
        /// 统一设置成功业务，成功并赋值
        /// </summary>
        /// <param name="data"></param>
        DataShell<TResult> Succeed(TResult data, string info);

        /// <summary>
        /// 统一设置失败业务
        /// </summary>
        DataShell<TResult> Failed();

        /// <summary>
        /// 统一设置失败业务，带入错误信息
        /// </summary>
        /// <param name="errorInfo"></param>
        DataShell<TResult> Failed(string errorInfo);

        /// <summary>
        /// 统一设置失败业务，带入一堆错误信息
        /// </summary>
        /// <param name="errorInfo"></param>
        DataShell<TResult> Failed(IList<string> errorInfo);

        /// <summary>
        /// 统一设置失败业务，带入错误信息，只返回基本错误信息
        /// </summary>
        /// <param name="error"></param>
        DataShell<TResult> Failed(Exception error);

        /// <summary>
        /// 统一设置失败业务，带入错误信息，可自定义错误信息处理委托
        /// </summary>
        /// <param name="error"></param>
        DataShell<TResult> Failed(Exception error, Func<Exception, string> errorInfoMaker);

        /// <summary>
        /// 设置一个信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        DataShell<TResult> AddInfo(string info);

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        DataShell<TResult> AddInfoDetail(string detail);

        /// <summary>
        /// 清空信息
        /// </summary>
        /// <returns></returns>
        DataShell<TResult> ClearInfo();

        /// <summary>
        /// 清空异常
        /// </summary>
        /// <returns></returns>
        DataShell<TResult> ClearExceptions();
    }
}
