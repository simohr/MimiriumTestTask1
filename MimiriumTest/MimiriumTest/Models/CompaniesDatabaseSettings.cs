using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimiriumTest.Models
{
	public class CompaniesDatabaseSettings : ICompaniesDatabaseSettings
	{
		public string CompaniesCollectionName { get; set ; }
		public string CompaniesCounterCollectionName { get; set ; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}

	public interface ICompaniesDatabaseSettings
	{
		string CompaniesCollectionName { get; set; }
		string CompaniesCounterCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}
