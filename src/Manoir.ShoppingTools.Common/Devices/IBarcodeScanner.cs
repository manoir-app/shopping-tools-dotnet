using Manoir.ShoppingTools.Common.BarCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Common.Devices
{
    public class ReadBarcode
    {
        public ReadBarcode(string code)
        {
            this.Code = code;
            this.CodeType = BarCodeHelper.IdentifyCodeType(code);
        }

        public string CodeType { get; set; }
        public string Code { get; set; }
    }

    public interface IBarcodeScanner
    {
        public class ReadBarcodeEventArgs : EventArgs {
            public ReadBarcode BarCode { get; set; }
        }

        event EventHandler<ReadBarcode> BarcodeRead;

        event EventHandler Faulted;

        void Init();
    }
}
