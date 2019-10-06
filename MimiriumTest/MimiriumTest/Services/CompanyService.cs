using MimiriumTest.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimiriumTest.Services
{
	public class CompanyService
	{
		private MongoClient client;
		private readonly IMongoCollection<Company> _companies;
		private readonly IMongoCollection<CompaniesCounter> _companiesCounter;

		public CompanyService(ICompaniesDatabaseSettings settings)
		{
			client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_companies = database.GetCollection<Company>(settings.CompaniesCollectionName);
			_companiesCounter = database.GetCollection<CompaniesCounter>(settings.CompaniesCounterCollectionName);
		}

		public List<Company> Get() =>
			_companies.Find(company => true).ToList();

		public Company Get(long id) =>
			_companies.Find<Company>(company => company.Id == id).FirstOrDefault();

		public List<Company> GetByName(string name)
		{
			if (name.StartsWith('*') && name.EndsWith('*'))
			{
				string nameOnly = name.Trim('*');
				return _companies.Find(company => company.Name.ToLower().Contains(nameOnly.ToLower())).ToList();
			}
			else if (name.StartsWith('*'))
			{
				return _companies.Find(company => company.Name.ToLower().EndsWith(name.Substring(1).ToLower())).ToList();
			}
			else if (name.EndsWith('*'))
			{
				return _companies.Find(company => company.Name.ToLower().StartsWith(name.Substring(0, name.Length - 1).ToLower())).ToList();
			}
			else
			{
				return _companies.Find(company => company.Name.ToLower() == (name.ToLower())).ToList();
			}
		}

		public Company Create(Company company)
		{
			using (var session = client.StartSession())
			{
				session.StartTransaction();
				try
				{
					var filter = Builders<CompaniesCounter>.Filter.Empty;
					var update = new BsonDocument("$inc", new BsonDocument { { "counter", 1 } });
					CompaniesCounter counterObj = _companiesCounter.FindOneAndUpdate<CompaniesCounter>(filter, update);
					company.Id = counterObj.Counter + 1;
					_companies.InsertOne(company);

					session.CommitTransaction();
				}
				catch (System.Exception e)
				{
					session.AbortTransaction();
					return null;
				}

				return company;
			}
		}

		public void Update(long id, Company companyIn) =>
			_companies.ReplaceOne(company => company.Id == id, companyIn);

		public void Remove(long id) =>
			_companies.DeleteOne(company => company.Id == id);

	}
}
