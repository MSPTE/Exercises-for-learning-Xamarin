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
using Android.Support.V7.Widget;

namespace Dining
{
    class RestaurantAdapter : RecyclerView.Adapter
    {
        private List<Restaurant> restaurants;
        public event EventHandler<int> ItemClick;
        
        public RestaurantAdapter(List<Restaurant> restaurants)
        {
            this.restaurants = restaurants;

        }

        public override int ItemCount
        {
            get
            {
                return restaurants.Count;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater.Inflate(Resource.Layout.RestaurantItem, parent, false);

            return new RestaurantViewHolder(view, OnItemClick);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = (RestaurantViewHolder)holder;
            viewHolder.tvName.Text = restaurants[position].Name;
            viewHolder.ratingBar.Rating = restaurants[position].Rating;
        }

        private void OnItemClick(int position)
        {
            if (ItemClick != null)
            {
                ItemClick(this, position);
            }
        }
    }
}