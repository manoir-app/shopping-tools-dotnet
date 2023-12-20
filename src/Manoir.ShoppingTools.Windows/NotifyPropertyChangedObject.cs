using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Windows
{
    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                var evt = PropertyChanged;
                if (evt != null) evt(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
