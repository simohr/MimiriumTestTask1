using Microsoft.AspNetCore.Mvc;
using MiriumTest.Models;
using MiriumTest.Services;
using System.Collections.Generic;

namespace MiriumTest.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly CompanyService _companySerivce;

		public CompaniesController(CompanyService companyService)
		{
			_companySerivce = companyService;
		}

		[HttpGet]
		public ActionResult<List<Company>> Get() =>
			_companySerivce.Get();

		[HttpGet("{id:length(24)}", Name = "GetCompany")]
		public ActionResult<Company> Get(string id)
		{
			var company = _companySerivce.Get(id);

			if(company == null)
			{
				return NotFound();
			}

			return company;
		}

		[HttpPost]
		public ActionResult<Company> Create(Company company)
		{
			_companySerivce.Create(company);

			return CreatedAtRoute("GetCompany", new { id = company.Id.ToString() }, company);
		}

		[HttpPut("{id:length(24)}")]
		public IActionResult Update(string id, Company companyIn)
		{
			var company = _companySerivce.Get(id);

			if(company == null)
			{
				return NotFound();
			}

			_companySerivce.Update(id, companyIn);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var company = _companySerivce.Get(id);

			if(company == null)
			{
				return NotFound();
			}

			_companySerivce.Remove(company.Id);

			return NoContent();
		}
	}
}
