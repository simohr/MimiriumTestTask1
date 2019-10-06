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
			saveButton.Click += (sender, args) => { Finish(); };
			titleText.Text = "Edit Company";
			nameText.Text = item.Name;
			vatText.Text = item.Vat;
		}

	}
}