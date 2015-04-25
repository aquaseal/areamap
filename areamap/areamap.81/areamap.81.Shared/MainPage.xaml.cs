using areamap._81.ViewModel;
using Esri.ArcGISRuntime.Geometry;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
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

            Messenger.Default.Register<MapPoint>(this, (location) =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    // zoom to current location
                    mapView.SetViewAsync(location, 100);
                });
            });
        }

        public MainViewModel VM
        {
            get { return (MainViewModel)DataContext; }
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