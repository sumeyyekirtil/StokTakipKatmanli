using Microsoft.EntityFrameworkCore;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Data;
using StokTakipKatmanli.Service.Abstract;
using System.Linq.Expressions;

namespace StokTakipKatmanli.Service.Concrete
{
	public class CategoryService : ICategoryService //abstract kalıtım alındı using ile
	
		//implement interface
	{
		public readonly DatabaseContext _context; //data layered using added

		public CategoryService(DatabaseContext context)
		{
			_context = context;
		}

		public void AddCategory(Category category)
		{
			_context.Categories.Add(category);
		}

		public List<Category> GetCategories()
		{
			return _context.Categories.ToList();
		}

		public List<Category> GetCategories(Expression<Func<Category, bool>> expression)
		{
			return _context.Categories.Where(expression).ToList();
		}

		public Category GetCategory(int id)
		{
			return _context.Categories.Find(id); //find ile bul
		}

		public Category GetCategoryByProduct(int id)
		{
			return _context.Categories.Where(c => c.IsActive && c.Id == id).Include(c => c.Products).FirstOrDefault(); //id ye göre gelenleri al
		}

		public void RemoveCategory(Category category)
		{
			_context.Categories.Remove(category);
		}

		public int Save()
		{
			return _context.SaveChanges();
		}

		public void UpdateCategory(Category category)
		{
			_context.Categories.Update(category);
		}
	}
}
