using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Account.Core.Core
{
    /// <summary>
    /// 公共错误
    /// </summary>
    public static class PubError
    {
        public static readonly string DBOPERR = "数据库操作错误";

        /* 错误分类：（只涉及大分类。不考虑细节，这一阶段重点考虑 接口）
         * 
         * 数据库操作错误，创建错误，更新错误，查询错误-查询不到，其他错误，数据层业务错误-缺少数据，数据层业务错误-数据不符
         * 重点记录：类别，摘要，参数
         */
        public static readonly IDictionary<string, IErrorDic> ErrorDic = new Dictionary<string, IErrorDic>() {
            [""] = CreateError("ERR-00200001", "DB", "未定义的错误"),
            ["ERR-00200001"] = CreateError("ERR-00200001", "DB", "定义的常规错误"),
            ["ERR-00200101"] = CreateError("ERR-00200100", "DB", "数据库语句错误"),
            ["ERR-00200201"] = CreateError("ERR-00200200", "DB", "数据库创建数据失败"),
            ["ERR-00200301"] = CreateError("ERR-00200300", "DB", "数据库更新，结果不符合更新预期 响应行数=0"),

            [""] = CreateError("ERR-0030002", "BLLError", "业务错误"),
        };

        /// <summary>
        /// 创建错误
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorCate"></param>
        /// <param name="errorContent"></param>
        /// <param name="errorAbstract"></param>
        /// <param name="errorTemplate"></param>
        /// <returns></returns>
        public static IErrorDic CreateError(string errorCode, string errorCate, string errorContent, string errorAbstract = "", string errorTemplate = "")
        {
            var err = new StaticError() { ErrorCode = errorCode, ErrorCate = errorCate, ErrorAbstract = errorAbstract, ErrorTemplate = errorTemplate };
            if (string.IsNullOrWhiteSpace(err.ErrorAbstract)) { err.ErrorAbstract = DBOPERR; }
            return err;
        }

        /// <summary>
        /// 链接一个错误，编码找不到就生成一个新的错误
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDetail"></param>
        /// <param name="errorFunc"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public static IErrorDic DBError(string errorCode, string errorFunc, string errorDetail = "", params object[] paramData)
        {
            IErrorDic temp = new StaticError() { ErrorCode = errorCode, ErrorCate = "DB", ErrorFunc = errorFunc };
            IErrorDic err = ErrorDic.ContainsKey(errorCode) ? ErrorDic[errorCode] : temp;
            err.ErrorDetail = errorDetail;


            return err;
        }
    }



    public class StaticError : IErrorDic
    {
        public string ErrorCode { get; set; } = "";
        public string ErrorFunc { get; set; }
        public string ErrorFuncCN { get; set; }
        public string ErrorLevel { get; set; }
        public string ErrorCate { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorContent { get; set; }
        public string ErrorPath { get; set; }
        public string ErrorTemplate { get; set; }
        public string ErrorAbstract { get; set; }

        public string ToStr()
        {
            return $"ErrorCode:{ErrorCode}, ErrorFunc:{ErrorFuncCN}[{ErrorFunc}] ErrorContent: {ErrorContent} - {ErrorDetail}";
        }

        public override string ToString()
        {
            return $"ErrorCode:{ErrorCode}, ErrorContent: {ErrorContent} - {ErrorDetail}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IErrorDic
    {
        string ErrorCate { get; set; }
        string ErrorCode { get; set; }
        string ErrorFunc { get; set; }
        string ErrorTemplate { get; set; }
        string ErrorDetail { get; set; }
        string ErrorContent { get; set; }
        string ErrorAbstract { get; set; }
        string ToString();
        string ToStr() => $"ErrorCode:{ErrorCode}, ErrorFunc:[{ErrorFunc}] ErrorContent: {ErrorContent} - {ErrorDetail}";
    }
}
