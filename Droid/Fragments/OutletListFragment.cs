using System;
using System.IO;
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

using DSoft.Messaging;

using MyPatchSG.BL.Globals;
using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.Droid.Activities;
using MyPatchSG.Droid.Adapters;
using MyPatchSG.Droid.ViewModels;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Globals;

namespace MyPatchSG.Droid.Fragments
{
    public class OutletListFragment : Fragment
    {
        IMenu menu;

        List<vwOutletList> outletList = new List<vwOutletList>();
        List<vwOutletListViewModel> outletListViewModels = new List<vwOutletListViewModel>();

        RecyclerView recyclerView;
        OutletListAdapter adapter;
        int SelectedIndex = -1;

        Android.Support.V7.Widget.SearchView _searchView;

        Android.App.ProgressDialog progress;

        public OutletListFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var masterDB = GlobalsAndroid.GetMasterDBInstance();
            if (masterDB != null)
            {
                outletList = masterDB.GetOutletList();

                foreach (var item in outletList)
                {
                    var viewModel = new vwOutletListViewModel(item);
                    outletListViewModels.Add(viewModel);
                }

                SelectedIndex = -1;
            }

            var newEventHandler = new MessageBusEventHandler()
            {
                EventId = "OutletListSort",
                EventAction = SortOutletListEventHandler
            };

            MessageBus.Default.Register(newEventHandler);

            var outletItemViewEventHandler = new MessageBusEventHandler()
            {
                EventId = "OutletListItemViewAppeared",
                EventAction = OutletListItemViewAppearedEventHandler
            };

            MessageBus.Default.Register(outletItemViewEventHandler);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            HasOptionsMenu = true;
            var view = inflater.Inflate(Resource.Layout.fragment_outletlist, null);

            recyclerView = (RecyclerView)view.FindViewById(Resource.Id.recyclerview_outletlist);

            // Plug in the linear layout manager:
            var layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);

            // Plug in my adapter:
            adapter = new OutletListAdapter(outletListViewModels, this);
            adapter.ItemClick += OnItemClick;
            adapter.ItemProjectCodeClick += OnItemProjectCodeClick;
            recyclerView.SetAdapter(adapter);
            
            return view;
        }

        void OnItemClick(object sender, OutletListItemSelectedEventArgs e)
        {
            vwOutletListViewModel selectedOutletViewModel = e.outletItem;
            selectedOutletViewModel.Selected = true;

            outletListViewModels[e.Position].Selected = true;
            if (SelectedIndex != -1)
            {
                outletListViewModels[SelectedIndex].Selected = false;
                Activity.RunOnUiThread(() =>
                {
                    adapter.NotifyItemChanged(SelectedIndex);
                });
            }

            Activity.RunOnUiThread(() =>
            {
                adapter.NotifyItemChanged(e.Position);
            });
            SelectedIndex = e.Position;

            GlobalsAndroid.SelectedOutletListItem = selectedOutletViewModel;

            ShowProgressDialog();

            Activity.RunOnUiThread(() =>
            {
                var outletItemActivity = new Intent(this.Context, typeof(OutletListItemViewActivity));
                StartActivity(outletItemActivity);
            });
        }
        void OnItemProjectCodeClick(object sender, OutletListItemProjectCodeClickedEventArgs e)
        {
            string ProjectCode = e.ProjectCode;
            GlobalsAndroid.FilterProjectCode = ProjectCode;

            Activity.RunOnUiThread(() =>
            {
                var filterActivity = new Intent(this.Context, typeof(FilterActivity));
                StartActivity(filterActivity);
            });
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.fragment_outletlist_menu, menu);

            var item = menu.FindItem(Resource.Id.outletlist_action_search);

            var searchItem = MenuItemCompat.GetActionView(item);
            _searchView = searchItem.JavaCast<Android.Support.V7.Widget.SearchView>();

            _searchView.QueryTextChange += (sender, e) =>
            {
                var keyword = _searchView.Query;
                var resultList = outletList.FindAll(delegate (vwOutletList outlet)
                {
                    return outlet.getCustomerID().ToLower().Contains(keyword.ToLower()) || outlet.getCustomerName().ToLower().Contains(keyword.ToLower());
                });

                outletListViewModels.Clear();
                SelectedIndex = -1;
                foreach (var outlet in resultList)
                {
                    var viewModel = new vwOutletListViewModel(outlet);
                    outletListViewModels.Add(viewModel);
                }

                // Plug in my adapter:
                adapter = new OutletListAdapter(outletListViewModels, this);
                adapter.ItemClick += OnItemClick;
                adapter.ItemProjectCodeClick += OnItemProjectCodeClick;
                recyclerView.SetAdapter(adapter);
            };

            var expandListener = new SearchViewExpandListener();
            expandListener.SearchViewMenuItemActionCollapsed += OnSearchViewMenuItemActionCollapsed;
            expandListener.SearchViewMenuItemActionExpanded += OnSearchViewMenuItemActionExpanded;

            MenuItemCompat.SetOnActionExpandListener(item, expandListener);

            this.menu = menu;

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.outletlist_action_filter:
                    OnFilterClicked();
                    return true;
                case Resource.Id.outletlist_action_sort:
                    OnSortClicked();
                    return true;
                default:
                    return false;
            }
        }

        private void OnSearchViewMenuItemActionCollapsed(object sender, SearchViewMenuItemActionCollapseEventArgs e)
        {
            // Show Filter & Sort switch action control
            this.menu.FindItem(Resource.Id.outletlist_action_filter).SetVisible(true);
            this.menu.FindItem(Resource.Id.outletlist_action_sort).SetVisible(true);

            // Reload contact list data
        }

        private void OnSearchViewMenuItemActionExpanded(object sender, SearchViewMenuItemActionExpandEventArgs e)
        {
            // Hide Filter & Sort switch action control
            this.menu.FindItem(Resource.Id.outletlist_action_filter).SetVisible(false);
            this.menu.FindItem(Resource.Id.outletlist_action_sort).SetVisible(false);
        }

        private void OnQueryTextChanged(object sender, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs e)
        {

        }

        private void OnFilterClicked()
        {
            GlobalsAndroid.FilterProjectCode = "";

            Activity.RunOnUiThread(() =>
            {
                var filterActivity = new Intent(this.Context, typeof(FilterActivity));
                StartActivity(filterActivity);
            });
        }

        private void OnSortClicked()
        {
            Activity.RunOnUiThread(() =>
            {
                var sortActivity = new Intent(this.Context, typeof(SortActivity));
                StartActivity(sortActivity);
            });
        }
        private void SortOutletListEventHandler(object sender, MessageBusEvent e)
        {
            SortOutletList();
        }

        private void OutletListItemViewAppearedEventHandler(object sender, MessageBusEvent e)
        {
            HideProgressDialog();
        }

        private void SortOutletList()
        {
            string orderby = "";
            switch (GlobalsAndroid.SortBy)
            {
                case "OutletID":
                    orderby = "CUSTOMER_ID";
                    break;
                case "OutletName":
                    orderby = "CUSTOMER_NAME";
                    break;
                case "BAT":
                    orderby = "FIS_PER";
                    break;
                case "Premium":
                    orderby = "HP_PER";
                    break;
                case "MR":
                    orderby = "FIS_MR_PER";
                    break;
            }

            if (orderby != "")
            {
                outletList.Clear();

                var masterDB = GlobalsAndroid.GetMasterDBInstance();
                if (masterDB != null)
                {
                    outletList = masterDB.GetOutletListSortedBy(orderby);
                }

                outletListViewModels.Clear();
                SelectedIndex = -1;

                foreach (var item in outletList)
                {
                    var viewModel = new vwOutletListViewModel(item);
                    outletListViewModels.Add(viewModel);
                }

                this.Activity.RunOnUiThread(() => {
                    adapter = new OutletListAdapter(outletListViewModels, this);
                    adapter.ItemClick += OnItemClick;
                    adapter.ItemProjectCodeClick += OnItemProjectCodeClick;
                    recyclerView.SetAdapter(adapter);
                });
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            //base.OnSaveInstanceState(outState);
        }

        private void ShowProgressDialog()
        {
            Activity.RunOnUiThread(() =>
            {
                progress = new Android.App.ProgressDialog(this.Context);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Please wait...");
                progress.SetCancelable(false);
                progress.Show();
            });
        }
        private void HideProgressDialog()
        {
            Activity.RunOnUiThread(() =>
            {
                progress.Hide();
                progress.Dismiss();
            });
        }
    }

    public class SearchViewExpandListener : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
    {
        public event EventHandler<SearchViewMenuItemActionCollapseEventArgs> SearchViewMenuItemActionCollapsed;
        public event EventHandler<SearchViewMenuItemActionExpandEventArgs> SearchViewMenuItemActionExpanded;

        public SearchViewExpandListener()
        {

        }

        public bool OnMenuItemActionCollapse(IMenuItem item)
        {
            if (this.SearchViewMenuItemActionCollapsed != null)
            {
                this.SearchViewMenuItemActionCollapsed(this, new SearchViewMenuItemActionCollapseEventArgs { });
            }

            return true;
        }

        public bool OnMenuItemActionExpand(IMenuItem item)
        {
            if (this.SearchViewMenuItemActionExpanded != null)
            {
                this.SearchViewMenuItemActionExpanded(this, new SearchViewMenuItemActionExpandEventArgs { });
            }

            return true;
        }
    }

    public class SearchViewMenuItemActionCollapseEventArgs : EventArgs
    {

    }

    public class SearchViewMenuItemActionExpandEventArgs : EventArgs
    {

    }
}
