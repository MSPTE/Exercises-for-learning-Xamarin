using System;
using Java.Lang;
using Android.Support.V4.App;

namespace Clock
{
	public class ClockAdapter : Android.Support.V4.App.FragmentPagerAdapter
	{
        Fragment[] fragments;
        ICharSequence[] titles;

        public ClockAdapter(FragmentManager fm, Fragment[] fragments, ICharSequence[] titles)
			: base(fm)
		{
            this.fragments = fragments;
            this.titles = titles;
		}

		public override int Count
		{
			get
			{
                return fragments.Length;
			}
		}

		public override Fragment GetItem(int position)
		{
            return fragments[position];
		}

		public override ICharSequence GetPageTitleFormatted(int position)
		{
            return titles[position];
		}
	}
}