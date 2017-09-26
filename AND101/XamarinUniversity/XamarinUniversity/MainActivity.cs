using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using Android.Content;

namespace XamarinUniversity
{
    [Activity(Label = "XamarinUniversity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        List<Instructor> instructors;
        ListView lvInstrutor;
        //ArrayAdapter<Instructor> adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            instructors = InstructorData.Instructors;
            //adapter = new ArrayAdapter<Instructor>(this, Android.Resource.Layout.SimpleListItem1, instructors);
            lvInstrutor = FindViewById<ListView>(Resource.Id.lv_instructor);
            lvInstrutor.FastScrollEnabled = true;
            lvInstrutor.Adapter = new InstructorAdapter(instructors);
            lvInstrutor.ItemClick += OnInstructorListViewClick;
        }

        private void OnInstructorListViewClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //int itemPos = e.Position;
            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetMessage(instructors[itemPos].Name);
            //dialog.SetNeutralButton("Ok", (senderAlert, args) => { });
            //dialog.Show();
            Intent intent = new Intent(this, typeof(InstructorDetailsActivity));
            intent.PutExtra("position", e.Position);
            this.StartActivity(intent);
        }
    }
}

