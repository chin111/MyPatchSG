
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Runtime;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;

using MyPatchSG.Droid.Activities;

namespace MyPatchSG.Droid.Fragments
{
    public class UOMFragment : Fragment
    {
        IMenu menu;

        public UOMFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            HasOptionsMenu = true;
            var view = inflater.Inflate(Resource.Layout.fragment_uom, null);

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.fragment_uom_menu, menu);

            this.menu = menu;

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return false;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            //base.OnSaveInstanceState(outState);
        }
    }
}
