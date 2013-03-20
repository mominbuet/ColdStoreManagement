using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ExportBillInvoices
    {
        public ExportBillInvoices() { }

        private int _SLNO;
        private int _BillID;
        private string _InvoiceNo;
        private double _InvoiceValue;
        private double _InvoiceQty;
        private string _ExpNo;
        private string _Comment;

        public int SLNO
        {
            get { return _SLNO; }
            set { _SLNO = value; }
        }
        public int BillID
        {
            get { return _BillID; }
            set { _BillID = value; }
        }
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo=value; }
        }
        public double InvoiceValue
        {
            get { return  _InvoiceValue; }
            set { _InvoiceValue=value; }
        }
        public double InvoiceQuantity
        {
            get { return _InvoiceQty; }
            set { _InvoiceQty=value; }
        }
        public string ExpNo
        {
            get { return _ExpNo; }
            set { _ExpNo=value; }
        }
        public string Comment
        {
            get { return _Comment; }
            set { _Comment=value; }
        }
    }
}
