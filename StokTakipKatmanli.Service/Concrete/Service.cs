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
	//generic - genel yapı c# olan bir kural
	public class Service<T> : IService<T> where T : class, IEntity, new()
		//Iservice kalıtım alındığından - implament interface
		//T nesnesi şartla üretildi
		//IEntity - abstract using
	{
		public readonly DatabaseContext _context; //data layered using
												 
		//generate constructor
		public Service(DatabaseContext context)
		{
			_context = context;
		}

		public void Add(T entity)
		{
			_context.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			await _context.AddAsync(entity); //make method async
		}

		public void Delete(T entity)
		{
			_context.Remove(entity);
		}

		public T Find(int id)
		{
			return _context.Set<T>().Find(id); //hatalarda return ile çözüm sağlanır
		}

		public async Task<T> FindAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().FirstOrDefault(expression); //get ile gelen ilk veya boş ise döndür
		}

		public List<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public List<T> GetAll(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().Where(expression).ToList(); //şart ile listele
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync(); //ef core using added
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
		{
			return await _context.Set<T>().Where(expression).ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(expression);
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			_context.Update(entity);
		}
	}
}
