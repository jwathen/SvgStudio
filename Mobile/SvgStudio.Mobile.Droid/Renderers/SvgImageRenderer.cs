using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SvgStudio.Mobile.Core.UI.Controls;
using SvgStudio.Mobile.Droid.Renderers;
using System.Threading.Tasks;
using System.ComponentModel;
using Com.Caverock.Androidsvg;
using Android.Graphics;

[assembly: ExportRenderer(typeof(SvgImage), typeof(SvgImageRenderer))]
namespace SvgStudio.Mobile.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class SvgImageRenderer : ViewRenderer<SvgImage, ImageView>
    {
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        private SvgImage _formsControl
        {
            get
            {
                return Element as SvgImage;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "SvgMarkup" || e.PropertyName == "SvgMarkupAccessor")
            {
                DrawImage();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SvgImage> e)
        {
            base.OnElementChanged(e);
            DrawImage();
        }

        protected void DrawImage()
        {
            if (_formsControl != null && (_formsControl.SvgMarkup != null || _formsControl.SvgMarkupAccessor != null))
            {
                Task.Run(() =>
                {
                    try
                    {
                        string markup = null;
                        if (!string.IsNullOrWhiteSpace(_formsControl.SvgMarkup))
                        {
                            markup = _formsControl.SvgMarkup;
                        }
                        else if (_formsControl.SvgMarkupAccessor != null)
                        {
                            markup = _formsControl.SvgMarkupAccessor();
                        }
                        if (markup == null)
                        {
                            return null;
                        }

                        var svg = SVG.GetFromString(markup);

                        var width = PixelToDP((int)_formsControl.Width <= 0 ? 100 : (int)_formsControl.Width);
                        var height = PixelToDP((int)_formsControl.Height <= 0 ? 100 : (int)_formsControl.Height);

                        float documentWidth = svg.DocumentWidth;
                        float documentHeight = svg.DocumentHeight;

                        if (documentWidth == -1 || documentHeight == -1 && svg.DocumentViewBox != null)
                        {
                            documentWidth = svg.DocumentViewBox.Width();
                            documentHeight = svg.DocumentViewBox.Height();
                        }

                        svg.SetDocumentViewBox(0, 0, documentWidth, documentHeight);
                        svg.SetDocumentWidth(width.ToString());
                        svg.SetDocumentHeight(height.ToString());

                        Bitmap bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                        Canvas canvas = new Canvas(bitmap);
                        RectF viewport = new RectF(0, 0, svg.DocumentWidth, svg.DocumentHeight);
                        svg.RenderToCanvas(canvas, viewport);

                        return bitmap;
                    }
                    catch (Exception ex)
                    {
                        Xamarin.Insights.Report(ex);
                        return null;
                    }
                }).ContinueWith(taskResult =>
                {
                    if (taskResult.Result != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var imageView = new ImageView(Context);

                            imageView.SetScaleType(ImageView.ScaleType.FitXy);
                            imageView.SetImageBitmap(taskResult.Result);
                            SetNativeControl(imageView);
                        });
                    }
                });
            }
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            return new SizeRequest(new Size(_formsControl.WidthRequest, _formsControl.WidthRequest));
        }

        /// <summary>
        /// http://stackoverflow.com/questions/24465513/how-to-get-detect-screen-size-in-xamarin-forms
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private int PixelToDP(int pixel)
        {
            var scale = Resources.DisplayMetrics.Density;
            return (int)((pixel * scale) + 0.5f);
        }
    }
}