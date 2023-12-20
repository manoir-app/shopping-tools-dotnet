using Home.Common.HomeAutomation;
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

    public class CodeBarScannedEventArgs : EventArgs
    {
        public string BarCode { get; set; }
        public string BarCodeType { get; set; }
    }


    public class CodeBarErrorEventArgs : EventArgs
    {
        public string ErrorCode { get; set; }
        public string ErrorDetails { get; set; }
        public Exception InnerException { get; set; }
    }

    public interface IBarCodeScanner: IDeviceBase
    {
        event EventHandler<CodeBarScannedEventArgs> BarCodeScanned;

        event EventHandler<CodeBarErrorEventArgs> ScanError;

        /// <summary>
        /// Met le scanner en veille.
        /// </summary>
        void Pause();

        /// <summary>
        /// Sort le scanner de la veille.
        /// </summary>
        void Resume();

        /// <summary>
        /// Vide la mémoire cache du scanner
        /// </summary>
        void ClearCache();
    }

}
