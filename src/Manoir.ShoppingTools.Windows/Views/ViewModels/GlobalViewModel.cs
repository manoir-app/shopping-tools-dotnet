using Manoir.ShoppingTools.Windows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manoir.ShoppingTools.Windows.Views.ViewModels
{
    public class GlobalViewModel : NotifyPropertyChangedObject
    {
        public ICommand BackToList = new SwitchToViewCommand(null, null);




        private static GlobalViewModel _instance = new GlobalViewModel();
        public static GlobalViewModel Instance { get { return _instance; } }

        private GlobalViewModel() {

            var lst = new List<MainAppMenuItem>();
            lst.Add(new MainAppMenuItem()
            {
                ImageUrl = "/Assets/menu-home.svg",
                Description = "Accueil",
                Name = "Accueil",
                IsSelected = true,
                Command = new SwitchToViewCommand(null, null)
            });
            MainMenuItems = lst;

        }


        private List<MainAppMenuItem> _mainMenuItems;

        public List<MainAppMenuItem> MainMenuItems
        {
            get { return _mainMenuItems; }
            set { _mainMenuItems = value; OnPropertyChanged(); }
        }




        private string _currentPage;

        public string CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;

                foreach (var r in _mainMenuItems)
                {
                    var c = (r.Command as SwitchToViewCommand);
                    if (c != null)
                    {
                        if (c.Page == value)
                            r.IsSelected = true;
                        else
                            r.IsSelected = false;
                    }
                }
                OnPropertyChanged();
            }
        }


        private class SwitchToViewCommand : ICommand
        {
            private string _page = null;
            private object _model = null;

            public string Page
            {
                get
                {
                    return _page;
                }
            }




            public SwitchToViewCommand(string page, object viewModel)
            {
                _model = viewModel;
                _page = page;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                GlobalViewModel.Instance.CurrentPage = _page;
                //if (_model != null && _model is IRefreshableViewModel)
                //{
                //    try
                //    {
                //        (_model as IRefreshableViewModel).DoRefresh();
                //    }
                //    catch
                //    {

                //    }
                //}
                foreach (var r in GlobalViewModel.Instance.MainMenuItems)
                {
                    if ((r?.Command as SwitchToViewCommand)?.Page == _page)
                        r.IsSelected = true;
                    else
                        r.IsSelected = false;
                }
            }
        }

    }
}
