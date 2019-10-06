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

namespace MobileApp.Adapters
{
	class CompanyAdapter : BaseAdapter<Company>
	{
		List<Company> companies;
		Activity context;

		public CompanyAdapter(Activity context, List<Company> companies) : base()
		{
			this.context = context;
			this.companies = companies;
		}
		public override Company this[int position]
		{
			get { return companies[position]; }
		}

		public Company GetCompanyAtPosition(int position)
		{
			return companies[position];
		}

		public override int Count
		{
			get { return companies.Count; } 
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var company = companies[position];

			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.company_list_item, null);
			view.FindViewById<TextView>(Resource.Id.companyName).Text = company.Name;
			view.FindViewById<TextView>(Resource.Id.companyVat).Text = company.Vat;

			return view;
		}
	}
}