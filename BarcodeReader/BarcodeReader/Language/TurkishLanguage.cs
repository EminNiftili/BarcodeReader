using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Language
{
    public class TurkishLanguage : LanguageModel
    {
        public override string StartScreen_AddFile => "Dosya oluştur";

        public override string StartScreen_FtpSettings => "Ftp ayarları";

        public override string StartScreen_LanguageSettings => "Dil ayarları";

        public override string StartScreen_TableFilenameColumn => "Dosya adı";

        public override string StartScreen_TableOperationColumn => "İşlemler";

        public override string StartScreen_DeleteFileInPopUp => "Silmek istediğine emin misin?";

        public override string DocumentList_AddGoods => "Mal ekle";

        public override string DocumentList_TableBarcodeColumnName => "Barkod";

        public override string DocumentList_TableAddedDateColumnName => "Oluşturduğu tarihi";

        public override string DocumentList_TableCountName => "Say";

        public override string DocumentList_FileSave => "Kaydet";

        public override string DocumentList_Cancel => "İptal et";

        public override string DocumentList_FileNameInputError => "Dosya adı yalnız harflardan oluşmalıdır!";

        public override string GeneralInformationPopUpHeader => "Bilgi";

        public override string DocumentList_GeneralPopUpOk => "Tamam";

        public override string GeneralPopUpConfirm => "Onayla";

        public override string GeneralPopUpCancel => "İptal";

        public override string DocumentList_CancelPopUpMessage => "Veriler kaydedilmeyecek!";

        public override string DocumentList_FileSavePopUpMessage => "Dosyayı kaydetmek için dosya adı yazmanız gerekir!";

        public override string DocumentList_ChangeCountInPopUpMessage => "Yeni miktarı giriniz! Malı kaldırmak için 0 girin!";

        public override string Recorder_KeepOpenPage => "Açık tut";

        public override string Recorder_ManualInputName => "Malın barkodu";

        public override string Recorder_ManualInputButton => "Ekle";
    }
}
