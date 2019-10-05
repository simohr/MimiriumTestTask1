using Microsoft.AspNetCore.Mvc;
using MimiriumTest.Models;
using MimiriumTest.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MimiriumTest.Controllers
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
		public ActionResult<List<Company>> Get()
		{
			if (String.IsNullOrEmpty(HttpContext.Request.Query["name"]))
			{
				return _companySerivce.Get();
			}
			else
			{
				string name = HttpContext.Request.Query["name"].ToString();
				var companies = _companySerivce.GetByName(name);

				return companies;
			}
		}

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
