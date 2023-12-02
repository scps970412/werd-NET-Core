namespace werd.Model
{
    public class SearchCondition
    {
        ///<sumary>
        /// 顯示筆數
        ///<sumary>
        public int PageSize { get; set; }
        ///<sumary>
        /// 頁數
        ///<sumary>
        public int Page { get; set; }
        ///<sumary>
        /// 供應商
        ///<sumary>
        public string Supplier { get; set; }
        ///<sumary>
        /// 統編
        ///<sumary>
        public string UnifiedBusinessNo { get; set; }
        ///<sumary>
        ///地址
        ///<sumary>
        public string Address { get; set; }
        ///<sumary>
        /// 電子信箱
        ///<sumary>
        public string Email { get; set; }
        ///<sumary>
        /// 負責人/聯絡人 
        ///<sumary>
        public string Name { get; set; }
        ///<sumary>
        /// 負責人電話/聯絡人電話
        ///<sumary>
        public string Phone { get; set; }
    }
}
