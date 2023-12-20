using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manoir.ShoppingTools.Windows.Models
{
    public class MainAppMenuItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICommand Command { get; set; }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
