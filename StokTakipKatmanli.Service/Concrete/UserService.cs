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
	public class UserService : IUserService
	{
		private readonly DatabaseContext _dbContext;

		public UserService(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void AddUser(User user)
		{
			_dbContext.Users.Add(user);
		}

		public User GetUser(int id)
		{
			return _dbContext.Users.Find(id);
		}

		public User GetUser(Expression<Func<User, bool>> expression)
		{
			return _dbContext.Users.FirstOrDefault(expression);
		}

		public List<User> GetUsers()
		{
			return _dbContext.Users.ToList();
		}

		public List<User> GetUsers(Expression<Func<User, bool>> expression)
		{
			return _dbContext.Users.Where(expression).ToList();
		}

		public void RemoveUser(User user)
		{
			_dbContext.Remove(user);
		}

		public int Save()
		{
			return _dbContext.SaveChanges();
		}

		public void UpdateUser(User user)
		{
			_dbContext.Users.Update(user);
		}
	}
}
