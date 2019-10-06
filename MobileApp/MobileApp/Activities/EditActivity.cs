using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Models;
using Newtonsoft.Json;

namespace MobileApp.Activities
{
	[Activity(Label = "EditActivity")]
	public class EditActivity : Activity
	{
		private Button saveButton;
		private Button cancelButton;
		private TextView titleText;
		private EditText nameText;
		private EditText vatText;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.company_edit);

			Company item = JsonConvert.DeserializeObject<Company>(Intent.GetStringExtra("CompanyItem"));

			saveButton = FindViewById<Button>(Resource.Id.saveButton);
			cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
			titleText = FindViewById<TextView>(Resource.Id.companyEditTitle);
			nameText = FindViewById<EditText>(Resource.Id.editCompanyName);
			vatText = FindViewById<EditText>(Resource.Id.editCompanyVat);

			cancelButton.Click += (sender, args) => { Finish(); };
			saveButton.Click += (sender, args) => { OnSaveClick(item.Id); };
			titleText.Text = Resources.GetString(Resource.String.edit_form_name);
			nameText.Text = item.Name;
			vatText.Text = item.Vat;
		}

		public async void OnSaveClick(long id)
		{
			Company newComapany = new Company
			{
				Id = id,
				Name = nameText.Text,
				Vat = vatText.Text
			};

			HttpClient client = new HttpClient();
			string url = "http://192.168.100.4:12109/api/companies/" + id;
			var uri = new Uri(url);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(newComapany);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			await client.PutAsync(uri, content);
			client.Dispose();
			Finish();
		}

	}
}