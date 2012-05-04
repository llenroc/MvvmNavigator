using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;

namespace MvvmNavigator.AppCode.ViewModels
{
    public class AlternatePageViewModel : WorkspaceViewModel
    {
        public AlternatePageViewModel(string title = "Welcome")
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
