using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public abstract class StudioStepBase : ViewModelBase, IStudioStep
    {
        public StudioStepBase()
        {
            ChildSteps = new ObservableCollection<IStudioStep>();
            ChildSteps.CollectionChanged += ChildSteps_CollectionChanged;
        }

        private void ChildSteps_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var child in e.NewItems.Cast<IStudioStep>())
                {
                    child.DecendentStepsChanged += Child_DecendentStepsChanged;
                }
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var child in e.OldItems.Cast<IStudioStep>())
                {
                    child.DecendentStepsChanged -= Child_DecendentStepsChanged;
                }
            }

            if (DecendentStepsChanged != null)
            {
                DecendentStepsChanged(this, null);
            }
        }

        private void Child_DecendentStepsChanged(object sender, EventArgs e)
        {
            if (DecendentStepsChanged != null)
            {
                DecendentStepsChanged(this, e);
            }
        }

        public ObservableCollection<IStudioStep> ChildSteps { get; private set; }

        public string DisplayText { get; protected set; }

        public event EventHandler DecendentStepsChanged;

        public abstract void Start(ContentView content, Action callback);

        public IEnumerable<IStudioStep> GetDescendentsAndSelf()
        {
            yield return this;
            foreach(var child in ChildSteps)
            {
                foreach(var decendent in child.GetDescendentsAndSelf())
                {
                    yield return decendent;
                }
            }
        }
    }
}
