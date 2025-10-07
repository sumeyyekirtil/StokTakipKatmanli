using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipKatmanli.Core.Entities
{
	public interface IEntity
	{
		// Interface : arayüz, uzaktan kumanda tuşlarına benzer
		//Her class kullanmak için id li interface oluşturuldu
		public int Id { get; set; }
	}
}
