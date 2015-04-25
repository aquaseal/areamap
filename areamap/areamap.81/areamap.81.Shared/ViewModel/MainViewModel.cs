using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Symbology;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.Generic;

namespace areamap._81.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region properties
        private Map myMap;

        public Map MyMap
        {
            get
            {
                if (myMap == null)
                    myMap = App.Current.Resources["MyMap"] as Map;
                return myMap;
            }

            set
            {
                if (myMap == value)
                    return;

                myMap = value;
                RaisePropertyChanged(() => MyMap);
            }
        }

        private LocationDisplay locationDisplay;

        public LocationDisplay LocationDisplay
        {
            get
            {
                if (locationDisplay == null)
                {
                    locationDisplay = new LocationDisplay()
                    {
                        AutoPanMode = AutoPanMode.CompassNavigation,
                        DefaultSymbol = App.Current.Resources["gpsSym"] as SimpleMarkerSymbol
                    };

                    locationDisplay.LocationProvider.LocationChanged += OnLocationChanged;
                    locationDisplay.IsEnabled = true;
                }

                return locationDisplay;
            }
            set { Set(() => LocationDisplay, ref locationDisplay, value); }
        }

        private List<MapPoint> gpsPoints = new List<MapPoint>();

        public List<MapPoint> GPSPoints
        {
            get { return gpsPoints; }
            set { Set(() => GPSPoints, ref gpsPoints, value); }
        }

        private double area;

        public double Area
        {
            get { return area; }
            set { Set(() => Area, ref area, value); }
        }
        #endregion  // properties


        #region commands
        private RelayCommand mapViewLoaded;

        public RelayCommand MapViewLoaded
        {
            get
            {
                return mapViewLoaded
                    ?? (mapViewLoaded = new RelayCommand(
                    () =>
                    {
                        areaPolyGraphic.Symbol = App.Current.Resources["polySym"] as SimpleFillSymbol;
                        (MyMap.Layers["AreaLayer"] as GraphicsLayer).Graphics.Add(areaPolyGraphic);
                    }));
            }
        }

        #endregion  // commands

        #region events
        private void OnLocationChanged(object sender, LocationInfo e)
        {
            if (e.Location.IsEqual(previousLocation) || e.Location == null)
                return;

            GPSPoints.Add(e.Location);
            previousLocation = e.Location;
            Messenger.Default.Send<MapPoint>(e.Location);

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Polygon p = new Polygon(GPSPoints);
                PolygonBuilder builder = new PolygonBuilder(p);
                areaPolyGraphic.Geometry = builder.ToGeometry();
                Area = GeometryEngine.GeodesicArea(p);
            });
        }
        #endregion  // events

        #region private variables
        private MapPoint previousLocation;
        Graphic areaPolyGraphic = new Graphic();
        #endregion  // private variables
    }
}