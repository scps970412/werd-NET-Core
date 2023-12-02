using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace werd.Model
{
    public class Supplier
    {
        /// <summary>
        /// 建立人員
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string Updater { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [StringLength(200, ErrorMessage = "備註不得超過 200 字")]
        public string Remark { get; set; }
        /// <summary>
        /// 供應商ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 供應商
        /// </summary>
        [RequiredAttribute(ErrorMessage = "供應商為必填"), StringLength(30, ErrorMessage = "供應商不得超過 30 字")]
        public string SupplierName { get; set; }
        /// <summary>
        /// 統一編號
        /// </summary>
        [RequiredAttribute(ErrorMessage = "統一編號為必填"), StringLength(8, MinimumLength = 8, ErrorMessage = "統一編號必須為 8 字"), RegularExpression("^[0-9]+$", ErrorMessage = "必须是数字")]
        public string UnifiedBusinessNo { get; set; }
        /// <summary>
        /// 電子郵件
        /// </summary>
        [RequiredAttribute(ErrorMessage = "電子郵件為必填"), EmailAddressAttribute(ErrorMessage = "必须是信箱格式"), StringLength(50, ErrorMessage = "電子郵件不得超過 50 字")]
        public string Email { get; set; }
        /// <summary>
        /// 供應商地址 1
        /// </summary>
        [RequiredAttribute(ErrorMessage = "供應商地址 1 為必填"), StringLength(100, ErrorMessage = "供應商地址 1 不得超過 100 字")]
        public string Address1 { get; set; }
        /// <summary>
        /// 供應商地址 2
        /// </summary>
        [StringLength(100, ErrorMessage = "供應商地址 2 不得超過 100 字")]
        public string Address2 { get; set; }
        /// <summary>
        /// 負責人
        /// </summary>
        [RequiredAttribute(ErrorMessage = "負責人為必填"), StringLength(20, ErrorMessage = "負責人不得超過 20 字")]
        public string Head { get; set; }
        /// <summary>
        /// 負責人電話 1
        /// </summary>
        [StringLength(20, ErrorMessage = "負責人電話 1 不得超過 20 字")]
        public string HeadPhone1 { get; set; }
        /// <summary>
        /// 負責人電話 2
        /// </summary>
        [StringLength(20, ErrorMessage = "負責人電話 2 不得超過 20 字")]
        public string HeadPhone2 { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        [RequiredAttribute(ErrorMessage = "聯絡人為必填"), StringLength(20, ErrorMessage = "聯絡人不得超過 20 字")]
        public string ContactPerson { get; set; }
        /// <summary>
        /// 聯絡人電話 1
        /// </summary>
        [RequiredAttribute(ErrorMessage = "聯絡人電話 1 為必填"), StringLength(020, ErrorMessage = "聯絡人電話 1 不得超過 20 字")]
        public string ContactPersonPhone1 { get; set; }
        /// <summary>
        /// 聯絡人電話 2
        /// </summary>
        [StringLength(20, ErrorMessage = "聯絡人電話 2 不得超過 20 字")]
        public string ContactPersonPhone2 { get; set; }
        /// <summary>
        /// 其他聯絡方式 1
        /// </summary>
        [StringLength(100, ErrorMessage = "其他聯絡方式 1 不得超過1020 字")]
        public string OtherContact1 { get; set; }
        /// <summary>
        /// 其他聯絡方式 2
        /// </summary>
        [StringLength(100, ErrorMessage = "其他聯絡方式 2 不得超過 100 字")]
        public string OtherContact2 { get; set; }
    }
}
