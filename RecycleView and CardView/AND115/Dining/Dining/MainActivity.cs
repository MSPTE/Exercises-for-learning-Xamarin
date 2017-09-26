using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Content;

namespace Dining
{
    [Activity(Label = "Dining", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private RecyclerView recyclerView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerView.SetLayoutManager(linearLayoutManager);
            var adapter = new RestaurantAdapter(SampleData.GetRestaurants());
            adapter.ItemClick += OnItemClick;
            recyclerView.SetAdapter(adapter);
        }

        private void OnItemClick(object sender, int position)
        {
            System.Diagnostics.Debug.WriteLine(position);
        }
    }
}

