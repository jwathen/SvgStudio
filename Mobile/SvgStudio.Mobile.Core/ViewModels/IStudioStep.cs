using System;
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
        string DisplayText { get; }
        ObservableCollection<IStudioStep> ChildSteps { get; }
        Task Start(ContentView content);
    }
}
