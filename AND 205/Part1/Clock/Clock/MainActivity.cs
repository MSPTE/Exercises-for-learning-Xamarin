﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Clock
{
	[Activity(Label = "Clock", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			var menuListView = FindViewById<ListView>(Resource.Id.menuListView);

			//
			// Built-in layout file SimpleListItem1 contains a TextView and nothing else
			//
			menuListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new[] { "Time", "Stopwatch", "About" }); 

			menuListView.ItemClick += OnMenuClick;
		}

		void OnMenuClick(object sender, AdapterView.ItemClickEventArgs e)
		{
            System.Type type;
            switch (e.Position)
            {
                case 0:
                    type = typeof(TimeActivity);
                    break;

                case 1:
                    type = typeof(StopwatchActivity);
                    break;

                case 2:
                    type = typeof(AboutActivity);
                    break;

                default:
                    return;
            }
            Intent intent = new Intent(this, type);
            this.StartActivity(intent);
		}
	}
}