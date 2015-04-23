using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace areamap._81
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            var lp = this.mapView.LocationDisplay.LocationProvider;
            lp.LocationChanged += OnLocationChanged;

            areaPolyGraphic.Symbol = this.Resources["polySym"] as SimpleFillSymbol;
            (mapView.Map.Layers["AreaLayer"] as GraphicsLayer).Graphics.Add(areaPolyGraphic);
        }

        private async void OnLocationChanged(object sender, Esri.ArcGISRuntime.Location.LocationInfo e)
        {
            if (e.Location.IsEqual(previousLocation))
                return;

            previousLocation = e.Location;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    this.mapView.SetViewAsync(e.Location, 100);

                    gpsPoints.Add(e.Location);
                    Polygon p = new Polygon(gpsPoints);
                    PolygonBuilder builder = new PolygonBuilder(p);
                    areaPolyGraphic.Geometry = builder.ToGeometry();

                    double polyArea = GeometryEngine.GeodesicArea(p);
                    areaText.Text = (polyArea * 10.7639104).ToString() + " sq ft";
                });
        }

        private PointCollection pc = new PointCollection();
        Graphic areaPolyGraphic = new Graphic();
        List<MapPoint> gpsPoints = new List<MapPoint>();
        MapPoint previousLocation;

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
