using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessService.MerchantService
{
    public interface IEmployee
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        string MerchantID { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        string StoreID { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        string EmployeeID { get; set; }
        /// <summary>
        /// 员工名
        /// </summary>
        string EmployeeName { get; set; }
    }
}
