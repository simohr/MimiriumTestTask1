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
		Android.Support.V7.Widget.SearchView searchView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			Log.Debug("SO", "HELLO");
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			listView = FindViewById<ListView>(Resource.Id.companiesList);
			InitializeListView();

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Click += FabOnClick;

			searchView = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.companySearchView);
			searchView.QueryTextSubmit += (sender, args) => 
			{
				if (string.IsNullOrEmpty(args.NewText))
				{
					InitializeListView();
				}
				else
				{
					GetCompaniesByName(args.NewText);
				}
				args.Handled = true;
			};
		}

		public async void GetCompaniesByName(string name)
		{
			HttpClient client = new HttpClient();
			string url = "http://192.168.100.4:12109/api/companies?name=" + name;
			var uri = new Uri(url);
			HttpResponseMessage response;

			response = client.GetAsync(uri).Result;
			var content = await response.Content.ReadAsStringAsync();
			companies = JsonConvert.DeserializeObject<List<Company>>(content);

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
			var comapny = adapter.GetCompanyAtPosition(info.Position);

			if (menuItemName == "Edit")
			{
				var intent = new Intent(this, typeof(EditActivity));
				intent.PutExtra("CompanyItem", JsonConvert.SerializeObject(comapny));
				StartActivity(intent);
			}
			else
			{
				OnDeleteClick(comapny);
			}
			return true;
		}

		protected override void OnResume()
		{
			base.OnResume();
			InitializeListView();
		}
		private async void InitializeListView()
		{
			HttpClient client = new HttpClient();
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			string url = "http://192.168.100.4:12109/api/companies";
			var uri = new Uri(url);
			//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response;

			response = client.GetAsync(uri).Result;
			var content = await response.Content.ReadAsStringAsync();
			companies = JsonConvert.DeserializeObject<List<Company>>(content);
			Log.Debug("SO", content);

			listView.Adapter = new CompanyAdapter(this, companies);
			RegisterForContextMenu(listView);
		}

		private void FabOnClick(object sender, EventArgs eventArgs)
		{
			StartActivity(typeof(AddCompany));
		}

		private async void OnDeleteClick(Company company)
		{
			HttpClient client = new HttpClient();
			string url = "http://192.168.100.4:12109/api/companies/" + company.Id;
			var uri = new Uri(url);;
			HttpResponseMessage response;
			response = await client.DeleteAsync(uri);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				Toast.MakeText(this, Resource.String.message_success, ToastLength.Short).Show();
			} 
			else
			{
				Toast.MakeText(this, Resource.String.message_error, ToastLength.Short).Show();
			}
			InitializeListView();
			client.Dispose();
		}
	}
}