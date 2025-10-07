using StokTakipKatmanli.Core.Entities;
using System.Linq.Expressions;

namespace StokTakipKatmanli.Service.Abstract
{
	//Yapılmak istenen işlemleri sırayla tanımlıyoruz
	public interface ICategoryService
	{
		//<> ile entities katmanı using kullanılıyor
		List<Category> GetCategories();
		List<Category> GetCategories(Expression<Func<Category, bool>> expression);
		Category GetCategory(int id);
		Category GetCategoryByProduct(int id);
		void AddCategory(Category category);
		void UpdateCategory(Category category);
		void RemoveCategory(Category category);
		int Save();
	}
}
