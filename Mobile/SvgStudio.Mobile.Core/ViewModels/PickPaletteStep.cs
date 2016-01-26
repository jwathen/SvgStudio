using SvgStudio.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class PickPaletteStep : ViewModelBase, IStudioStep
    {
        public PickPaletteStep()
        {
            ChildSteps = new ObservableCollection<IStudioStep>();
        }

        public string DisplayText
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Palette Palette { get; set; }

        public void Start(ContentView content, Action callback)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<IStudioStep> ChildSteps { get; private set; }
    }
}
