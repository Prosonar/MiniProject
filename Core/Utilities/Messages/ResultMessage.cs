using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    /// <summary>
    /// Turkish messages for information
    /// </summary>
    public static class ResultMessage
    {
        public const string SuccessMessage = "İşlem Başarılı";
        public const string Errormessage = "İşlem Başarısız. Lütfen daha sonra tekrar deneyiniz.";

        public const string DatabaseError = "Veritabanı hatası.";
        public const string DatabaseSaveChangesError = "Veritabanı kaydetme hatası.";

        public const string DataNotFound = "Veri bulunamadı.";
        public const string AlreadyHaveCategory = "Bu ürün zaten bu kategoriye sahip. ";
    }
}
