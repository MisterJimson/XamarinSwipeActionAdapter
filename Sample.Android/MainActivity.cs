using System;
using Android.App;
using Android.OS;
using Com.Wdullaer.Swipeactionadapter;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;

namespace Sample.Android
{
    [Activity(Label = "Sample.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity, SwipeActionAdapter.ISwipeActionListener
    {
        protected SwipeActionAdapter mAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            string[] content = new string[20];
            for (int i = 0; i < 20; i++)
                content[i] = "Row " + (i + 1);

            ArrayAdapter<string> stringAdapter = 
                new ArrayAdapter<string>(this, Resource.Layout.row_bg, Resource.Id.text, new List<string>(content));

            mAdapter = new SwipeActionAdapter(stringAdapter);
            mAdapter.SetSwipeActionListener(this)
                    .SetDimBackgrounds(true)
                    .SetListView(this.ListView);

            this.ListAdapter = mAdapter;

            mAdapter.AddBackground(SwipeDirection.DirectionFarLeft, Resource.Layout.row_bg_left_far)
               .AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Layout.row_bg_left)
               .AddBackground(SwipeDirection.DirectionFarRight, Resource.Layout.row_bg_right_far)
               .AddBackground(SwipeDirection.DirectionNormalRight, Resource.Layout.row_bg_right);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            Toast.MakeText(this, "Clicked " + mAdapter.GetItem(position), ToastLength.Short).Show();
        }

        public bool HasActions(int p0, SwipeDirection p1)
        {
            if (p1.IsLeft)
                return true;
            if (p1.IsRight)
                return true;

            return false;
        }

        public void OnSwipe(int[] positionList, SwipeDirection[] directionList)
        {
            for (int i = 0; i < positionList.Length; i++)
            {
                SwipeDirection direction = directionList[i];
                int position = positionList[i];
                string dir = "";

                if (direction == SwipeDirection.DirectionFarLeft)
                {
                    dir = "Far left";
                }
                else if (direction == SwipeDirection.DirectionNormalLeft)
                {
                    dir = "Left";
                }
                else if (direction == SwipeDirection.DirectionNormalLeft)
                {
                    dir = "Far right";
                }
                else if (direction == SwipeDirection.DirectionNormalLeft)
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Test Dialog").SetMessage("You swiped right").Create().Show();
                    dir = "Right";
                }
                
                Toast.MakeText(this, dir + " swipe Action triggered on " + mAdapter.GetItem(position), ToastLength.Short).Show();
                mAdapter.NotifyDataSetChanged();
            }
        }

        public bool ShouldDismiss(int p0, SwipeDirection p1)
        {
            return p1 == SwipeDirection.DirectionNormalLeft;
        }
    }
}

