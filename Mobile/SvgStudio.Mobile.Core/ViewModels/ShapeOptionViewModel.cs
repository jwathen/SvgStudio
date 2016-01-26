using SvgStudio.Mobile.Core.Exceptions;
using SvgStudio.Shared.Drawing;
using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class ShapeOptionViewModel : ViewModelBase
    {
        private readonly IStorageRepository _db;
        private readonly double _width;
        private readonly double _height;
        private bool _isSelected;

        public ShapeOptionViewModel(Shape shape, double width, double height, IStorageRepository db)
        {
            Shape = shape;
            _db = db;
            _width = width;
            _height = height;
            SelectCommand = new Command(() => IsSelected = true);
            ShowLicenseCommand = new Command(ShowLicense);
            PreviewMarkupAccessor = RenderPreviewMarkup;
        }

        public Shape Shape { get; private set; }
        public Func<string> PreviewMarkupAccessor { get; private set; }
        public Command SelectCommand { get; set; }
        public Command ShowLicenseCommand { get; set; }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    FirePropertyChanged();
                }
            }
        }

        private string RenderPreviewMarkup()
        {
            var renderDesignResult = Shape.Render(null, "Preview");
            var svg = renderDesignResult.AsStandaloneSvg(_width, _height);
            return XmlHelper.RenderDocument(svg, true);
        }

        private void ShowLicense()
        {
            Task.Run(() =>
            {
                var contentLicense = _db.LoadContentLicenceByShapeId(Shape.StorageId);
                Shared.StorageModel.License license = null;
                if (contentLicense != null)
                {
                    license = _db.LoadLicense(contentLicense.LicenseId);
                    if (license != null)
                    {
                        StringBuilder licensingInfo = new StringBuilder();
                        licensingInfo.AppendLine(string.Format("\"{0}\" is licensed under:", Shape.Name));
                        licensingInfo.AppendLine();
                        licensingInfo.AppendLine(license.LicenseName);
                        licensingInfo.AppendLine(license.LicenseUrl);
                        if (license.AttributionRequired)
                        {
                            licensingInfo.AppendLine();
                            licensingInfo.AppendLine(contentLicense.AttributionName);
                            licensingInfo.AppendLine(contentLicense.AttributionUrl);
                        }
                        return licensingInfo.ToString();
                    }
                }
                return null;
            }).ContinueWith(task =>
            {
                if (task.Result != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Licensing Information", task.Result, "Ok");
                    });
                }
            });
        }
    }
}
