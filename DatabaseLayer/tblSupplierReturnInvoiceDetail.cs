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
    
    public partial class tblSupplierReturnInvoiceDetail
    {
        public int SupplierReturnInvoiceDetailID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int SupplierReturnInvoiceID { get; set; }
        public int SupplierInvoiceDetailID { get; set; }
        public int ProductID { get; set; }
        public int PurchaseReturnQuantity { get; set; }
        public double PurchaseReturnUnitPrice { get; set; }
    
        public virtual tblStock tblStock { get; set; }
        public virtual tblSupplierInvoice tblSupplierInvoice { get; set; }
        public virtual tblSupplierInvoiceDetail tblSupplierInvoiceDetail { get; set; }
        public virtual tblSupplierReturnInvoice tblSupplierReturnInvoice { get; set; }
    }
}
