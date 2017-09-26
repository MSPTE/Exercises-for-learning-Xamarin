using Android.App;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Content;
using Android.Runtime;
using System;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Location;
using System.Text;
using System.Collections.Generic;

namespace DroidMapping
{
    [Activity(Label = "DroidMapping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback,
        Android.Gms.Location.ILocationListener,
        Android.Locations.ILocationListener,
        GoogleApiClient.IConnectionCallbacks,
        GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleMap map;
        MapFragment mapFragment;
        Marker currentLocationMarker;
        Marker lastGeoMarker;
        private GoogleApiClient apiClient;
        List<Marker> pointsOfInterest;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Find and load the map fragment
            mapFragment = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            mapFragment.GetMapAsync(this);

            if (GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this) == 0)
            {
                apiClient = new GoogleApiClient.Builder(this)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .AddApi(LocationServices.API)
                    .Build();
            }

        }

        private async void OnGetDetails(object sender, GoogleMap.MapClickEventArgs e)
        {
            Geocoder geocoder = new Geocoder(this);
            var results = await geocoder.GetFromLocationAsync(e.Point.Latitude, e.Point.Longitude, 1);

            if (results.Count > 0)
            {
                var result = results[0];
                if (lastGeoMarker == null)
                {
                    MarkerOptions markerOptios = new MarkerOptions()
                        .SetPosition(new LatLng(result.Latitude, result.Longitude))
                        .SetTitle(result.FeatureName)
                        .SetSnippet(GetAdress(result))
                        .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange));
                    lastGeoMarker = map.AddMarker(markerOptios);
                }
                else
                {
                    lastGeoMarker.Position = new LatLng(result.Latitude, result.Longitude);
                    lastGeoMarker.Title = result.FeatureName;
                    lastGeoMarker.Snippet = GetAdress(result);
                }
                lastGeoMarker.ShowInfoWindow();
            }
        }

        string GetAdress(Address result)
        {
            var sb = new StringBuilder();
            for (int index = 0; index < result.MaxAddressLineIndex; index++)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(result.GetAddressLine(index));
            }

            return sb.ToString();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            //map.MapType = GoogleMap.MapTypeHybrid;
            map.MapClick += OnGetDetails;
            map.UiSettings.ZoomControlsEnabled = true;
            map.MapLongClick += ShowPOI;
            pointsOfInterest = new List<Marker>();


            // Center on Xamarin HQ
            LatLng coord = new LatLng(37.797679, -122.401816);
            CameraUpdate update = CameraUpdateFactory.NewLatLngZoom(coord, 17);
            map.AnimateCamera(update);


            Criteria criteria = new Criteria()
            {
                Accuracy = Accuracy.Fine,
                PowerRequirement = Power.NoRequirement,
            };
            
            if (apiClient != null)
            {
                if (!apiClient.IsConnected)
                    apiClient.Connect();
            }
            else
            {
                LocationManager locationManager = this.GetSystemService(Context.LocationService) as LocationManager;
                locationManager.RequestLocationUpdates(5000, 100f, criteria, this, null);
                //locationManager.GetBestProvider(criteria, true);
            }
        }

        private async void ShowPOI(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            
            foreach (var point in pointsOfInterest)
                point.Remove();
            pointsOfInterest.Clear();

            LatLngBounds bounds = e.Point.GetBoundingBox(8000);

            var geocoder = new Geocoder(this);
            var results = await geocoder.GetFromLocationNameAsync("Starbucks", 5,
                bounds.Southwest.Latitude, bounds.Southwest.Longitude,
                bounds.Northeast.Latitude, bounds.Northeast.Longitude);

            foreach (var result in results)
            {
                var markerOptions = new MarkerOptions()
                    .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan))
                    .SetPosition(new LatLng(result.Latitude, result.Longitude))
                    .SetTitle(result.FeatureName)
                    .SetSnippet(GetAdress(result));

                pointsOfInterest.Add(map.AddMarker(markerOptions));
            }

        }

        public void OnLocationChanged(Location location)
        {
            LatLng coord = new LatLng(location.Latitude, location.Longitude);
            CameraUpdate update = CameraUpdateFactory.NewLatLngZoom(coord, 17);
            map.AnimateCamera(update);
            if (currentLocationMarker != null)
            {
                currentLocationMarker.Position = coord;
            }
            else
            {
                currentLocationMarker = map.AddMarker(new MarkerOptions()
                    .SetPosition(coord)
                    .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue))
                    .SetTitle("Current Position")
                    .SetSnippet("This is where you are now"));
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (apiClient != null && map != null)
            {
                apiClient.Connect();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (apiClient != null)
            {
                apiClient.Disconnect();
            }
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        public void OnConnected(Bundle connectionHint)
        {
            LocationRequest locationRequest = LocationRequest.Create()
                .SetPriority(LocationRequest.PriorityHighAccuracy)
                .SetInterval(5000)
                .SetSmallestDisplacement(100f);

            LocationServices.FusedLocationApi.RequestLocationUpdates(apiClient, locationRequest, this);
        }

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            throw new NotImplementedException();
        }
    }
}


