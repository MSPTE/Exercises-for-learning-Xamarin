using Android.App;

#if RELEASE
[assembly: MetaDataAttribute("com.google.android.maps.v2.API_KEY", Value="release_key_goes_here")]
#else
[assembly: MetaDataAttribute("com.google.android.maps.v2.API_KEY", Value= "AIzaSyBnFarH8bZ6pl032lUV7pfybnC1XlAul_M")]
#endif