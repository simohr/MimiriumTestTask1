using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Views;
using Android.Support.Design.Widget;
using System.Collections.Generic;
using MobileApp.Models;
using MobileApp.Adapters;
using MobileApp.Activities;
using Android.Content;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Android.Util;
using System.Net;

namespace MobileApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
		List<Company> companies = new List<Company>();
		ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			Log.Debug("SO", "HELLO");
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);
			InitializeListView();


			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Click += FabOnClick;
		}

		public async void InitializeListView()
		{
			listView = FindViewById<ListView>(Resource.Id.companiesList);

			//companies.Add(new Company()
			//{
			//	Id = 0,
			//	Name = "Mimirium",
			//	Vat = "12345221"
			//});
			//companies.Add(new Company()
			//{
			//	Id = 1,
			//	Name = "MyCompany1",
			//	Vat = "1111232"
			//});
			//companies.Add(new Company()
			//{
			//	Id = 2,
			//	Name = "MyCompany2",
			//	Vat = "1222221"
			//});

			HttpClient client = new HttpClient();
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			string url = "http://192.168.100.6:8080/api/companies";
			var uri = new Uri(url);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response;

			response = client.GetAsync(uri).Result;
			var content = await response.Content.ReadAsStringAsync();
			Log.Debug("SO", content);

			listView.Adapter = new CompanyAdapter(this, companies);
			RegisterForContextMenu(listView);
		}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			//base.OnCreateContextMenu(menu, v, menuInfo);
			if(v.Id == Resource.Id.companiesList)
			{
				string[] values = { "Edit", "Delete" };
				for (var i = 0; i < values.Length; i++)
					menu.Add(Menu.None, i, i, values[i]);
			}
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			var menuItemIndex = item.ItemId;
			string[] menuItems = { "Edit", "Delete" };
			var menuItemName = menuItems[menuItemIndex];
			var adapter = (CompanyAdapter)listView.Adapter;
			var listItemName = adapter.GetCompanyAtPosition(info.Position);

			if(menuItemName == "Edit")
			{
				var intent = new Intent(this, typeof(EditActivity));
				intent.PutExtra("CompanyItem", JsonConvert.SerializeObject(listItemName));
				StartActivity(intent);
			}
			else
			{ 
				Toast.MakeText(this, string.Format("Selected {0} for item {1}", menuItemName, listItemName), ToastLength.Short).Show();
			}
			return true;
		}

		private void FabOnClick(object sender, EventArgs eventArgs)
		{
			View view = (View)sender;
			StartActivity(typeof(AddCompany));
			//Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
			//	.SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
		}
	}
}