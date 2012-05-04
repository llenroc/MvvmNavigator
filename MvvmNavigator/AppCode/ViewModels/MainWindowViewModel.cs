using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using System.Windows.Input;

namespace MvvmNavigator.AppCode.ViewModels
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        public MainWindowViewModel()
        {
            _navVm = new WorkspaceNavigationViewModel(new StartPageViewModel("test"));

            SetupHotkeys();
        }

        WorkspaceNavigationViewModel _navVm;           
        public WorkspaceNavigationViewModel Navigator { get { return _navVm; } }

        #region Commands

        public RelayCommand _testCommand;
        public ICommand TestCommand
        {
            get { return _testCommand ?? (_testCommand = new RelayCommand(() => this.Test())); }
        }

        void Test()
        {
            WorkspaceViewModel toAdd = DateTime.Now.Second % 2 == 0 ? 
                new StartPageViewModel(DateTime.Now.ToLongTimeString()) as WorkspaceViewModel
                : new AlternatePageViewModel(DateTime.Now.ToLongTimeString()) as WorkspaceViewModel;
            _navVm.Workspaces.Add(toAdd);
            _navVm.ShowLastWorkspace();
        }

        #endregion Commands

        #region Hotkeys

        public void SetupHotkeys()
        {
            _hotkeys = new List<InputBinding>();

            _hotkeys.Add(new InputBinding(TestCommand, new KeyGesture(Key.N, ModifierKeys.Control, "Test")));
            _hotkeys.Add(new InputBinding(new RelayCommand(() => Navigator.PreviousWorkspace(),() => Navigator.CanPreviousWorkspace), new KeyGesture(Key.Left, ModifierKeys.Control, "Go Back")));
            _hotkeys.Add(new InputBinding(new RelayCommand(() => Navigator.NextWorkspace(), () => Navigator.CanNextWorkspace), new KeyGesture(Key.Right, ModifierKeys.Control, "Go Forward")));
        }

        List<InputBinding> _hotkeys;
        public List<InputBinding> Hotkeys
        {
            get { return _hotkeys; }
            set
            {
                if (value == _hotkeys || value == null) return;

                _hotkeys = value;
                base.RaisePropertyChanged("Hotkeys");
            }
        }

        #endregion
    }        
}
