using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Common.BarCodes
{
    public static class Ean13Helper
    {
        public const string CodeBarTypeEan13 = "EAN13";

        public static bool IsEan13(string barcode)
        {
            if (barcode.Length != 13)
                return false;

            return CheckBarCode(barcode);
        }

        public static string GetBarcodeChecksum(string barcode)
        {
            int oddTotal;
            int evenTotal;

            if (barcode.Length % 2 == 0)
            {
                oddTotal = SumNumPositionOdd(barcode);
                oddTotal = oddTotal * 3;
                evenTotal = sumOutsideOrdinals(barcode);
            }
            else
            {
                oddTotal = sumOutsideOrdinals(barcode);
                oddTotal = oddTotal * 3;
                evenTotal = SumNumPositionOdd(barcode);
            }

            int finalTotal = oddTotal + evenTotal;
            int modVal = finalTotal % 10;
            int checkSum = 10 - modVal;
            if (checkSum == 10)
            {
                return "0";
            }
            return checkSum.ToString();
        }


        private static int SumNumPositionOdd(string barcode)
        {
            int cumulativeVal = 0;
            for (int i = barcode.Length - 1; i > -1; i--)
            {
                if (i % 2 != 0)
                {
                    cumulativeVal += Convert.ToInt16(barcode[i] - '0');
                }
            }
            return cumulativeVal;
        }

        private static int sumOutsideOrdinals(string barcode)
        {
            int cumulativeVal = 0;
            for (int i = barcode.Length - 1; i > -1; i--)
            {
                if (i % 2 == 0)
                {
                    cumulativeVal += Convert.ToInt16(barcode[i] - '0');
                }
            }
            return cumulativeVal;
        }

        private static bool CheckBarCode(string barCode)
        {
            string barcodeSansCheckDigit = barCode.Substring(0, barCode.Length - 1);
            string checkDigit = barCode.Substring(barCode.Length - 1, 1);
            return GetBarcodeChecksum(barcodeSansCheckDigit) == checkDigit;
        }
    }
}
