using Manoir.ShoppingTools.Common.BarCodes;
using Manoir.ShoppingTools.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Cli
{
    public class ConsoleKeyboardBarcodeReader : IBarcodeScanner
    {
        static ConsoleKeyboardBarcodeReader()
        {

        }

        public event EventHandler<ReadBarcode> BarcodeRead;
        public event EventHandler Faulted;

        public static bool _stop = false;

        private void OnBarCodeRead(string barcode)
        {
            var evt = BarcodeRead;
            if (evt != null)
                BarcodeRead.Invoke(this, new ReadBarcode(barcode));
        }

        public void Init()
        {
            Thread t = new Thread(o =>
            {
                StringBuilder buffer = new StringBuilder();
                while (!_stop)
                {
                    while (!Console.KeyAvailable)
                    {
                        Thread.Sleep(15);
                    }

                    // Key is available - read it
                    var key = Console.ReadKey();
                    var t = key.KeyChar;

                    
                    if (t.Equals('\r') || t.Equals('\n') || t.Equals('\t'))
                    {
                        OnBarCodeRead(buffer.ToString());
                        buffer.Clear();
                    }
                    else if(!t.Equals('\0'))
                    {
                        buffer.Append(t);
                    }
                }


            });
            t.Start();
            
        }
    }
}
