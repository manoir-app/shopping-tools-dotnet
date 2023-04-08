using Manoir.ShoppingTools.Common.Devices;
using System;
using System.Threading;

namespace Manoir.ShoppingTools.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bc = new ConsoleKeyboardBarcodeReader();

            bc.Init();
            bc.BarcodeRead += Bc_BarcodeRead;

            while (true)
            {
                Thread.Sleep(50);
            }
        }

        private static void Bc_BarcodeRead(object sender, ReadBarcode e)
        {
            Console.WriteLine();
            Console.WriteLine($"Read : { e.Code} of type {e.CodeType}");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
        }
    }
}