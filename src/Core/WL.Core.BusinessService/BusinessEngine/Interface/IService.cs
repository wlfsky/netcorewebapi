using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessEngine
{
    /// <summary>
    /// 服务
    /// </summary>
    public interface IService
    {
        #region --请求方--
        /// <summary>
        /// 申请服务
        /// </summary>
        void Apply();
        /// <summary>
        /// 签约-请求方
        /// </summary>
        void RequesterContract();
        /// <summary>
        /// 确认-双方
        /// </summary>
        void RequesterConfirm();
        /// <summary>
        /// 邮寄发件
        /// </summary>
        void PostSend();
        /// <summary>
        /// 撤回
        /// </summary>
        void Cancel();
        /// <summary>
        /// 请求方删除
        /// </summary>
        void Del();
        /// <summary>
        /// 评价-请求方
        /// </summary>
        void RequesterEvaluate();
        #endregion

        #region --响应方--
        /// <summary>
        /// 获取
        /// </summary>
        void Acquire();
        void FishFor();//捕捞捞取，同Acquire方法
        
        /// <summary>
        /// 返还，恢复
        /// </summary>
        void Restore();
        /// <summary>
        /// 推荐
        /// </summary>
        void Recommend();
        /// <summary>
        /// 签约-响应方
        /// </summary>
        void RespondentContract();
        /// <summary>
        /// 确认-响应方
        /// </summary>
        void RespondentConfirm();
        /// <summary>
        /// 邮寄回客户
        /// </summary>
        void PostBack();
        /// <summary>
        /// 争议
        /// </summary>
        void Dispute();
        /// <summary>
        /// 指派，内部指派
        /// </summary>
        void Appoint();
        /// <summary>
        /// 评价-响应方
        /// </summary>
        void RespondentEvaluate();
        #endregion

        #region --系统--
        /// <summary>
        /// 完结
        /// </summary>
        void Finish();
        
        /// <summary>
        /// 冻结
        /// </summary>
        void Freeze();

        /// <summary>
        /// 归档
        /// </summary>
        void PlaceOnFile();
        #region --可见性--
        /// <summary>
        /// 单池可见性
        /// </summary>
        void PondShow();
        #endregion

        #region --可用性--
        /// <summary>
        /// 可用性
        /// </summary>
        void Enable();
        /// <summary>
        /// 删除
        /// </summary>
        void Destroy();
        #endregion
        #endregion


    }
}
