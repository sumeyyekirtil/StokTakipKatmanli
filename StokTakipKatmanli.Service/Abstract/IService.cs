using StokTakipKatmanli.Core.Entities;
using System.Linq.Expressions;

//Service layered -> project add -> class library new project
// Abctract and Concrete new folder added
//new referances -> data layered
//new referances -> WebUI layered -> Service layered
namespace StokTakipKatmanli.Service.Abstract
{
	//class: referance tipli
	//abstract: değer tipli
	public interface IService<T> where T : class, IEntity, new() //Service bir tip ile çalışacak T parametresi
	{
		//IService interface i dışarıdan T olarak bir parametre ile çalışacak, sart olan kodu buraya gelecek T tipinin şartlarını belirler, class olmalı IEntity interfacesi kullanmalı ve new lenebilir bir yapı olmalı(string olmamalı)

		//Senkron Metot İmzaları
		List<T> GetAll(); //list görünümü için
		List<T> GetAll(Expression<Func<T , bool>> expression);
		T Get(Expression<Func<T , bool>> expression);
		T Find(int id);
		void Add(T entity);
		void Delete(T entity);
		void Update(T entity);
		int SaveChanges();

		//Asenkron Metotlar : Task-görev
		Task<List<T>> GetAllAsync();
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
		Task<T> FindAsync(int id);
		Task<T> GetAsync(Expression<Func<T, bool>> expression);
		Task AddAsync(T entity); //dönüş değerini düzenlemek için
		//update ve delete async yok kod yazarken backend düzeltme yapılıyor
		Task<int> SaveChangesAsync();
	}
}
