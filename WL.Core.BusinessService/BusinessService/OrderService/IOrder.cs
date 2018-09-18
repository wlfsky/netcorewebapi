using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.DataShell;

namespace WL.Core.BusinessService.BusinessService.OrderService
{
    public interface IOrder
    {
        #region --数据--
        #region --订单元数据--
        /// <summary>
        /// 订单ID
        /// </summary>
        string OrderID { get; set; }
        /// <summary>
        /// 订单名
        /// </summary>
        string OrderName { get; set; }
        #endregion

        #region --订单发起者--

        #endregion

        #region --订单领取者--

        #endregion

        #region --订单消费者--


        #endregion
        #endregion

        #region --行为--
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns></returns>
        DataShell<IOrder> Create();

        #region --业务行为--
        #region --订单签约--
        /// <summary>
        /// 用户订单签约
        /// </summary>
        /// <returns></returns>
        DataShell<IOrder> UserSignContract();

        /// <summary>
        /// 商户订单签约
        /// </summary>
        /// <returns></returns>
        DataShell<IOrder> MarchentSignContract();
        #endregion


        #endregion

        #endregion
    }
}
