using Manoir.ShoppingTools.Common.BarCodes;
using Manoir.ShoppingTools.Common.Devices;
using Manoir.ShoppingTools.Windows.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static Manoir.ShoppingTools.Common.BarCodes.ComPortBarCodeScanner;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Manoir.ShoppingTools.Windows
{
    public sealed partial class MainViewControl : UserControl
    {
        public MainViewControl()
        {
            this.InitializeComponent();
            var t = new ComPortBarCodeScanner();
            t.Configure(Guid.Empty, "BarCodeTest", JsonConvert.SerializeObject(new ConfigScannerCOM()
            {
                Port = "COM3"
            }));
            t.BarCodeScanned += T_BarCodeScanned;
            _barCodeScanner = t;
        }

        private void T_BarCodeScanned(object sender, CodeBarScannedEventArgs e)
        {
            
        }

        public IBarCodeScanner _barCodeScanner = null;


        public Views.ViewModels.GlobalViewModel ViewModel { get; set; } = Views.ViewModels.GlobalViewModel.Instance;

        private void nvMainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var item = (args.SelectedItem as MainAppMenuItem);
                if (item != null)
                    item.Command.Execute("");
            }
        }

      
    }
}
