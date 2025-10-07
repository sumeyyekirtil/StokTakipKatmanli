using Microsoft.EntityFrameworkCore;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Data;
using StokTakipKatmanli.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipKatmanli.Service.Concrete
{
	public class ProductService : IProductService
	{
		private readonly DatabaseContext _context;

		public ProductService(DatabaseContext context)
		{
			_context = context;
		}

		public void AddProduct(Product product)
		{
			_context.Products.Add(product);
		}

		public Product GetProduct(int id)
		{
			return _context.Products.Find(id);
		}

		public Product GetProductByCategoryAndProductImages(int id)
		{
			return _context.Products.Where(c => c.IsActive && c.Id == id).Include(c => c.Category).FirstOrDefault(); //burada ürüne ürün resimleri de dahil edilecek
		}

		public List<Product> GetProducts()
		{
			return _context.Products.ToList();
		}

		public List<Product> GetProducts(Expression<Func<Product, bool>> expression)
		{
			return _context.Products.Where(expression).ToList();
		}

		public Product GetProductsByCategory(int id)
		{
			return _context.Products.Where(c => c.IsActive && c.Id == id).Include(c => c.Category).FirstOrDefault();
		}

		public void RemoveProduct(Product product)
		{
			_context.Remove(product);
		}

		public int Save()
		{
			return _context.SaveChanges();
		}

		public void UpdateProduct(Product product)
		{
			_context.Update(product);
		}
	}
}
