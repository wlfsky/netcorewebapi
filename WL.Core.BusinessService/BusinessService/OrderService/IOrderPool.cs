using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using WlToolsLib.DataShell;

namespace WL.Core.BusinessService.BusinessService.OrderService
{
    /// <summary>
    /// 订单池
    /// </summary>
    public interface IOrderPool
    {
        /// <summary>
        /// 自由订单队列
        /// </summary>
        List<IOrder> FreeOrderList { get; set; }

        /// <summary>
        /// 僵尸订单列表，长期无人领取
        /// </summary>
        List<IOrder> ZombieOrderList { get; set; }

        /// <summary>
        /// 签约订单
        /// </summary>
        List<IOrder> SignContractOrderList { get; set; }

        #region --行为--
        /// <summary>
        /// 插入订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        DataShell<bool> Push(IOrder order);

        /// <summary>
        /// 插回订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        DataShell<bool> PushBack(IOrder order);

        DataShell<IOrder> Claim(IOrder order);

        #endregion
    }
}
