﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public interface IStudioStep
    {
        event EventHandler DecendentStepsChanged;
        ObservableCollection<IStudioStep> ChildSteps { get; }
        IEnumerable<IStudioStep> GetDescendentsAndSelf();
        string DisplayText { get; }
        void Start(ContentView content, Action callback);
    }
}
