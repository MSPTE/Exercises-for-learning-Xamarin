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

#if RELEASE
[assembly: MetaDataAttribute("com.google.android.geo.API_KEY", Value="AIzaSyBnFarH8bZ6pl032lUV7pfybnC1XlAul_M")]
#else
[assembly: MetaDataAttribute("com.google.android.geo.API_KEY", Value="AIzaSyBnFarH8bZ6pl032lUV7pfybnC1XlAul_M")]
#endif