using StokTakipKatmanli.Core.Entities;
using System.Linq.Expressions;

namespace StokTakipKatmanli.Service.Abstract
{
	public interface IUserService
	{
		//<> ile entities katmanı using kullanılıyor
		List<User> GetUsers();
		List<User> GetUsers(Expression<Func<User, bool>> expression);
		User GetUser(int id);
		User GetUser(Expression<Func<User, bool>> expression); // post kullanımı için
		void AddUser(User user);
		void UpdateUser(User user);
		void RemoveUser(User user);
		int Save();
	}
}
