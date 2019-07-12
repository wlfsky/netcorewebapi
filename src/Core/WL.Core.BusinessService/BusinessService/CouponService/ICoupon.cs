using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessService.CouponService
{
    public interface ICoupon
    {
        /// <summary>
        /// 优惠券ID
        /// </summary>
        string CouponID { get; set; }
        /// <summary>
        /// 优惠券名
        /// </summary>
        string CouponName { get; set; }
    }
}
