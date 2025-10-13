using StokTakipKatmanli.Core.Entities;
using System.Linq.Expressions;

namespace StokTakipKatmanli.Service.Abstract
{
	public interface IProductService
	{
		//<> ile entities katmanı using kullanılıyor
		List<Product> GetProducts();
		List<Product> GetProducts(Expression<Func<Product, bool>> expression);
		Product GetProduct(int id);
		Product GetProductByCategory(int id); //product içinde category kullanımı sağlar
		Product GetProductByCategoryAndProductImages(int id); //resim eklemek için
		void AddProduct(Product product);
		void UpdateProduct(Product product);
		void RemoveProduct(Product product);
		int Save();
	}
}
