using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;

namespace DroidMapping
{
    [Activity(Label = "DroidMapping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        static readonly LatLng LOCATION_XAMARIN = new LatLng(37.80, -112.40);
        static readonly LatLng LOCATION_NEWYORK = new LatLng(40.77, -73.98);
        static readonly LatLng LOCATION_CHICAGO = new LatLng(41.88, -87.63);
        static readonly LatLng LOCATION_DALLAS = new LatLng(32.90, -97.03);
        static readonly LatLng LOCATION_SEATLE = new LatLng(47.61, -122.34);

        private GoogleMap googleMap;
        private MapFragment mapFragment;
        private Marker markerChicago;
        private Marker markerDallas;
        private Marker markerSeatle;

        public void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            this.googleMap.MapType = GoogleMap.MapTypeNormal;
            this.googleMap.MyLocationEnabled = true;
            
            this.googleMap.UiSettings.CompassEnabled = true;
            this.googleMap.UiSettings.MyLocationButtonEnabled = true;
            this.googleMap.UiSettings.ZoomControlsEnabled = true;
            //this.googleMap.UiSettings.SetAllGesturesEnabled(true);

            this.googleMap.AddMarker(new MarkerOptions().SetPosition(LOCATION_NEWYORK));
            this.googleMap.AddMarker(new MarkerOptions().SetPosition(LOCATION_XAMARIN)
                .SetTitle("Xamarin HQ")
                .SetSnippet("Where the magic happen")
                .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Icon)));
            markerChicago = this.googleMap.AddMarker(new MarkerOptions().SetPosition(LOCATION_CHICAGO)
                .SetTitle("Chicago")
                .Draggable(true)
                .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow)));
            markerDallas = this.googleMap.AddMarker(new MarkerOptions().SetPosition(LOCATION_DALLAS)
                .SetTitle("Dallas")
                .SetSnippet("This is the marker for Dallas")
                .InfoWindowAnchor(1, 1)
                .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue)));
            markerSeatle = this.googleMap.AddMarker(new MarkerOptions().SetPosition(LOCATION_SEATLE)
                .SetTitle("Seatle")
                .SetSnippet("Yeah, Seatle!!!")
                .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure)));

            this.googleMap.MarkerClick += MarkerClick;
            this.googleMap.InfoWindowClick += InfoWindowClick;
            this.googleMap.MapClick += MapClick;
            this.googleMap.MapLongClick += (sender, e) =>
            {
                this.googleMap.AnimateCamera(CameraUpdateFactory.ZoomOut(), 1000, null);
            };


            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom
                (LOCATION_SEATLE, this.googleMap.MaxZoomLevel);
            this.googleMap.MoveCamera(cameraUpdate);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void MarkerClick (object sender, GoogleMap.MarkerClickEventArgs e)
        {
            if (e.Marker.Equals(markerDallas))
            {
                markerDallas.Flat = !markerDallas.Flat;
            }
            else
            {
                e.Handled = false;
            }
        }

        public void InfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            if (e.Marker.Id == markerChicago.Id)
            {
                e.Marker.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRose));
            }
        }

        private void MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            if (!markerChicago.IsInfoWindowShown)
            {
                markerChicago.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow));
            }
        }
    }
}

