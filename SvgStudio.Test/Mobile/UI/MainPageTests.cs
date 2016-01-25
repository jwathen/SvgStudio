using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Mobile.Core.UI;
using SvgStudio.Mobile.Core.ViewModels;
using SvgStudio.Test.TestHelpers;
using SvgStudio.Test.TestHelpers.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Should;

namespace SvgStudio.Test.Mobile.UI
{
    public class MainPageTests
    {
        public void M()
        {
            ServerDatabase.Reset();
            TestDatabaseConnectionProvider db = new TestDatabaseConnectionProvider(true);
            var mobile = new MobileStorageRepository(db.GetConnection());

            var stepView = new ContentView();
            StudioViewModel vm = new StudioViewModel("16284653806660-fda6c4c1ae034e5ea", stepView, mobile);
            vm.Init();

            vm.Steps.First().Start(stepView);

            var doc = new XmlDocument();
            doc.LoadXml(vm.PreviewMarkup);
            var svg = Svg.SvgDocument.Open(doc);
        }
    }
}
