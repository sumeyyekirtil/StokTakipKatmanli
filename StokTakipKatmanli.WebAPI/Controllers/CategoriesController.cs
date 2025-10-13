using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Service.Abstract;


namespace StokTakipKatmanli.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly IService<Category> _categoryService;

		public CategoriesController(IService<Category> categoryService)
		{
			_categoryService = categoryService;
		}

		// GET: api/<CategoriesController>
		[HttpGet]
		public IEnumerable<Category> Get()
		{
			return _categoryService.GetAll();
		}

		// GET api/<CategoriesController>/5
		[HttpGet("{id}")]
		public Category Get(int id)
		{
			return _categoryService.Find(id);
		}

		// POST api/<CategoriesController>
		[HttpPost]
		public void Post([FromBody] Category value)
		{
			_categoryService.Add(value);
			_categoryService.SaveChanges();
		}

		// PUT api/<CategoriesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Category value)
		{
			_categoryService.Update(value);
			_categoryService.SaveChanges();
		}

		// DELETE api/<CategoriesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var kayit = Get(id);
			_categoryService.Delete(kayit);
			_categoryService.SaveChanges();
		}
	}
}
