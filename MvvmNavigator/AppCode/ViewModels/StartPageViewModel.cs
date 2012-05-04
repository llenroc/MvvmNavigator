using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;

namespace MvvmNavigator.AppCode.ViewModels
{
    public class StartPageViewModel : WorkspaceViewModel 
    {
        public StartPageViewModel(string title = "Welcome")
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
