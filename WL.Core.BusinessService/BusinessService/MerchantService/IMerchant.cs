using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessService.MerchantService
{
    public interface IMerchant
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        string MerchantID { get; set; }
        /// <summary>
        /// 商户名
        /// </summary>
        string MerchantName { get; set; }
    }
}
