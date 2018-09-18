using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessService.MerchantService
{
    /// <summary>
    /// 商户店铺
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// 商户
        /// </summary>
        string MerchantID { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        string StoreID { get; set; }
        /// <summary>
        /// 门店名
        /// </summary>
        string StoreName { get; set; }
    }
}
