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
    class RestaurantViewHolder : RecyclerView.ViewHolder
    {
        private Action<int> listener;

        public TextView tvName { get; set; }
        public RatingBar ratingBar { get; set; }

        public RestaurantViewHolder(View itemView, Action<int> listener) : base (itemView)
        {
            tvName = itemView.FindViewById<TextView>(Resource.Id.tv_name);
            ratingBar = itemView.FindViewById<RatingBar>(Resource.Id.rating_bar);

            this.listener = listener;
            itemView.Click += OnClick;
        }

        void OnClick(object sender, EventArgs e)
        {
            int position = base.AdapterPosition;
            if (position == RecyclerView.NoPosition)
                return;
            listener(position);
        }
    }
}