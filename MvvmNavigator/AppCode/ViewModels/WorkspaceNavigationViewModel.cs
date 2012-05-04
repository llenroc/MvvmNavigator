using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using System.Windows.Input;

namespace MvvmNavigator.AppCode.ViewModels
{
    public class WorkspaceNavigationViewModel : ObservableObject
    {
        public WorkspaceNavigationViewModel(WorkspaceViewModel startWorkspace, bool showNavButtons = true)
        {
            _showNavButtons = showNavButtons;
            Workspaces = new List<WorkspaceViewModel>();
            Workspaces.Add(startWorkspace);
            _currentWorkspace = Workspaces.First();
        }


        bool _showNavButtons;
        public bool ShowNavButtons
        {
            get { return _showNavButtons; }
            set
            {
                if (value == _showNavButtons) return;

                _showNavButtons = value;
                base.RaisePropertyChanged("ShowNavButtons");
            }
        }

        public List<WorkspaceViewModel> Workspaces { get; set; }    

        private WorkspaceViewModel _currentWorkspace;
        public WorkspaceViewModel CurrentWorkspace
        {
            get { return _currentWorkspace; }
            set
            {
                if (value == _currentWorkspace) return;
                if (!Workspaces.Contains(value)) throw new InvalidOperationException("CurrentWorkspace must be a Workspace in the Workspaces collection.");

                _currentWorkspace = value;
                base.RaisePropertyChanged("CurrentWorkspace");
            }
        }

        #region Commands

        public bool CanPreviousWorkspace { get { return Workspaces.IndexOf(CurrentWorkspace) > 0; } }
        public RelayCommand _previousWorkspaceCommand;
        public ICommand PreviousWorkspaceCommand
        {
            get { return _previousWorkspaceCommand ?? (_previousWorkspaceCommand = new RelayCommand(() => this.PreviousWorkspace(), () => this.CanPreviousWorkspace)); }
        }

        public void PreviousWorkspace()
        {
            if (!CanPreviousWorkspace) return;
            CurrentWorkspace = Workspaces[Workspaces.IndexOf(CurrentWorkspace) - 1];
        }

        public bool CanNextWorkspace { get { return Workspaces.IndexOf(CurrentWorkspace) < Workspaces.Count() - 1; } }
        public RelayCommand _nextWorkspaceCommand;
        public ICommand NextWorkspaceCommand
        {
            get { return _nextWorkspaceCommand ?? (_nextWorkspaceCommand = new RelayCommand(() => this.NextWorkspace(), () => this.CanNextWorkspace)); }
        }

        public void NextWorkspace()
        {
            if (!CanNextWorkspace) return;
            CurrentWorkspace = Workspaces[Workspaces.IndexOf(CurrentWorkspace) + 1];
        }

        #endregion Commands

        #region Methods

        public void ShowFirstWorkspace()
        {
            CurrentWorkspace = Workspaces.First();
        }

        public void ShowLastWorkspace()
        {
            CurrentWorkspace = Workspaces.Last();
        }

        public void ShowWorkspaceOrLast(int WorkspaceIndex)
        {
            if (WorkspaceIndex < 0) throw new ArgumentOutOfRangeException("WorkspaceIndex", "Value cannot be negative.");
            CurrentWorkspace = (WorkspaceIndex > Workspaces.Count - 1) ? Workspaces.Last() : Workspaces[WorkspaceIndex];
        }

        #endregion Methods
    }
}
