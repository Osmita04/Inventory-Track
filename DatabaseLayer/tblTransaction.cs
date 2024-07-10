//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTransaction
    {
        public int TransactionID { get; set; }
        public int FinancialYearID { get; set; }
        public int AccountHeadID { get; set; }
        public int AccountControlID { get; set; }
        public int AccountSubControlID { get; set; }
        public string InvoiceNo { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public System.DateTime TransectionDate { get; set; }
        public string TransectionTitle { get; set; }
        public int UserID { get; set; }
    
        public virtual tblAccountControl tblAccountControl { get; set; }
        public virtual tblAccountHead tblAccountHead { get; set; }
        public virtual tblAccountHead tblAccountHead1 { get; set; }
        public virtual tblAccountSubControl tblAccountSubControl { get; set; }
        public virtual tblFinancialYear tblFinancialYear { get; set; }
        public virtual tblFinancialYear tblFinancialYear1 { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
