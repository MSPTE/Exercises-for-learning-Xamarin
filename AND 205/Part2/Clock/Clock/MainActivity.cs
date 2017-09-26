using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;

namespace Clock
{
	[Activity(Label = "Clock", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Android.Support.V4.App.FragmentActivity
	{
        ViewPager viewPager;
        ClockAdapter adapter;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			var fragments = new Android.Support.V4.App.Fragment[]
				{
					new TimeFragment     (),
					new StopwatchFragment(),
					new AboutFragment    ()
				};

			var titles = CharSequence.ArrayFromStringArray(new [] 
				{ 
					"Time", 
					"Stopwatch", 
					"About"
				});

            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            adapter = new ClockAdapter(base.SupportFragmentManager, fragments, titles);
            viewPager.Adapter = adapter;

			//
			// TODO: Assign the ClockAdapter to the ViewPager's Adapter property
			//



		}
	}
}