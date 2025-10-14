namespace StokTakipKatmanli.WebAPIUsing.Tools
{
	//admin - controller larda kullanmak için tools - file helper eklendi (yeni metot tanımlamalarıyla kod çokluğundan kurtulmuş olunur)
	public class FileHelper
	{
		//resim ekleme - güncelleme yolu
		public static string FileLoader(IFormFile formFile) //resim dosyası yükleme işlemi için kod azaltmak için yeni metot tanımladık
		{ //static olması class adı verip hızla ulaşmak için
			string dosyaAdi = "";

			dosyaAdi = formFile.FileName; //geri döndürülen değere dosya adı eşitlendi
			string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/";
			using var stream = new FileStream(klasor + formFile.FileName, FileMode.Create); //yeni dosya olarak yükle
			formFile.CopyTo(stream); //yolu kopyala

			return dosyaAdi;
		}

		//resim silme yolu
		public static bool FileRemover(string fileName, string klasorYolu = "/wwwroot/Images/")
		{
			string klasor = Directory.GetCurrentDirectory() + klasorYolu + fileName;
			if (File.Exists(klasor)) //eğer sunucuda dosya varsa
			{
				File.Delete(klasor); //dosyayı sil
				return true; //silme başarılı
			}
			return false;
		}
	}
}
