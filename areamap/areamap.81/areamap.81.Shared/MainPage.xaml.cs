using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace areamap._81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            var lp = this.mapView.LocationDisplay.LocationProvider;
            lp.LocationChanged += OnLocationChanged;

            //todo use MVVM to add the current location to an observable collection

            //todo build out the polyline where appropaite
            //var polylineBuilder = new PolylineBuilder(polyline); // Create builder based on existing one
            //polylineBuilder.Parts[0].RemoveAt(0);
            //polylineBuilder.AddPoint(8, 4); // adding a point to last part
            //polyline = polylineBuilder.ToGeometry(); // get updated geometry


            //PolygonBuilder builder = new PolygonBuilder(polygon); // Create builder from existing polygon  
            //builder.AddPoint(new MapPoint(x, y)); // Add point to end  
            //builder.Parts[0].InsertPoint(index, new MapPoint(x, y)); // Insert point into specific location  
            //graphic.Geometry = builder.ToGeometry(); // Create geometry and assign it back to 

        }

        private async void OnLocationChanged(object sender, Esri.ArcGISRuntime.Location.LocationInfo e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    this.mapView.SetViewAsync(e.Location, 100);
                });
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
