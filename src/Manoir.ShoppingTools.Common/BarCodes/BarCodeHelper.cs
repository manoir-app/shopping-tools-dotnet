using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Common.BarCodes
{
    public static class BarCodeHelper
    {
        public static string IdentifyCodeType(string barCode)
        {
            return IdentifyCodeType(barCode, null);
        }

        public static string IdentifyCodeType(string barCode, string suggestedType)
        {
            if (string.IsNullOrWhiteSpace(barCode))
                return "unknown";

            if (Ean13Helper.IsEan13(barCode))
                return Ean13Helper.CodeBarTypeEan13;

            return "unknown";
        }
    }
}
