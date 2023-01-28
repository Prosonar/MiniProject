﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    public static class ResultMessage
    {
        public const string SuccessMessage = "İşlem Başarılı";
        public const string Errormessage = "İşlem Başarısız. Lütfen daha sonra tekrar deneyiniz.";

        public const string DatabaseError = "Veritabanı hatası.";
        public const string DatabaseSaveChangesError = "Veritabanı kaydetme hatası.";

        public const string DataNotFound = "Veri bulunamadı.";
    }
}
