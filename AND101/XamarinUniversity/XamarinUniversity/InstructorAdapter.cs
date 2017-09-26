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
using Android.Graphics.Drawables;
using System.IO;
using Java.Lang;

namespace XamarinUniversity
{
    class InstructorAdapter : BaseAdapter<Instructor>, ISectionIndexer
    {
        List<Instructor> instructors;
        Java.Lang.Object[] sectionHeaders;
        Dictionary<int, int> positionForSectionMap;
        Dictionary<int, int> sectionForPositionMap;
        //ImageView imgPhoto;
        //TextView tvName;
        //TextView tvSpecialty;


        public InstructorAdapter(List<Instructor> instructors)
        {
            this.instructors = instructors;
            sectionHeaders = SectionIndexerBuilder.BuildSectionHeaders(instructors);
            positionForSectionMap = SectionIndexerBuilder.BuildPositionForSectionMap(instructors);
            sectionForPositionMap = SectionIndexerBuilder.BuildSectionForPositionMap(instructors);
        }

        public override Instructor this[int position]
        {
            get
            {
                return instructors[position];
            }
        }

        public override int Count
        {
            get
            {
                return instructors.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                var inflater = LayoutInflater.From(parent.Context);
                view = inflater.Inflate(Resource.Layout.InstructorRow, parent, false);
            }
            ViewHolder holder = new ViewHolder();
            holder.Photo = view.FindViewById<ImageView>(Resource.Id.photoImageView);
            holder.Name = view.FindViewById<TextView>(Resource.Id.nameTextView);
            holder.Specialty = view.FindViewById<TextView>(Resource.Id.specialtyTextView);

            var instructor = instructors[position];
            Stream stream = parent.Context.Assets.Open(instructor.ImageUrl);
            //imgPhoto.SetImageDrawable(Drawable.CreateFromStream(stream, null));
            holder.Photo.SetImageDrawable(ImageAssetManager.Get(parent.Context, instructor.ImageUrl));
            holder.Name.Text = instructor.Name;
            holder.Specialty.Text = instructor.Specialty;

            return view;
        }

        public int GetPositionForSection(int sectionIndex)
        {
            return positionForSectionMap[sectionIndex];
        }

        public int GetSectionForPosition(int position)
        {
            return sectionForPositionMap[position];
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionHeaders;
        }
    }
}