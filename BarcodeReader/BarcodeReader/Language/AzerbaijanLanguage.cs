using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Language
{
    public class AzerbaijanLanguage : LanguageModel
    {
        public override string StartScreen_AddFile => "Yeni sənəd yarat";

        public override string StartScreen_FtpSettings => "Ftp tənzimləməsi";

        public override string StartScreen_LanguageSettings => "Dil seçimi";

        public override string StartScreen_TableFilenameColumn => "Faylın adı";

        public override string StartScreen_TableOperationColumn => "Əməliyyatlar";

        public override string StartScreen_DeleteFileInPopUp => "Silmək istədiyinizdən əminisiniz?";

        public override string DocumentList_AddGoods => "Mal əlavə et";

        public override string DocumentList_TableBarcodeColumnName => "Barkod";

        public override string DocumentList_TableAddedDateColumnName => "Əlavə edildiyi tarix";

        public override string General_SaveText => "Yadda saxla";

        public override string DocumentList_Cancel => "Ləğv et";

        public override string DocumentList_FileNameInputError => "Faylın adı yalnız hərflərdən ibarət ola bilər";

        public override string DocumentList_TableCountName => "Say";

        public override string GeneralInformationPopUpHeader => "Məlumat";

        public override string DocumentList_CancelPopUpMessage => "Heç bir məlumat yadda saxlanılmayacaq!";

        public override string GeneralPopUpConfirm => "Təsdiq";

        public override string GeneralPopUpCancel => "Ləğv";

        public override string DocumentList_FileSavePopUpMessage => "Faylı yadda saxlamaq üçün faylın adını təyin etməlisiniz!";

        public override string DocumentList_GeneralPopUpOk => "Bağla";

        public override string DocumentList_ChangeCountInPopUpMessage => "Yeni miqdarı daxil edin! Malı silmək üçün 0 daxil edin!";

        public override string Recorder_KeepOpenPage => "Açıq saxla";

        public override string Recorder_ManualInputName => "Malın barkodu:";

        public override string Recorder_ManualInputButton => "Əlavə et";

        public override string FtpSettings_UrlInput => "Ftp adresi";

        public override string FtpSettings_PortInput => "Ftp portu";

        public override string FtpSettings_UsernameInput => "İstifadəçi adı";

        public override string FtpSettings_PasswordInput => "Şifrə";

        public override string FtpSettings_PathInput => "Qovluğun adresi";

        public override string General_FtpConnectionError => "Ftp bağlantısı qurmaq mümkün olmadı. Zəhmət olmasa bağlantınızı yoxlayın!";

        public override string General_SuccesMessage => "Uğurlu əməliyyat!";

        public override string StartScreen_ExitApp => "Çıxış";
    }
}
