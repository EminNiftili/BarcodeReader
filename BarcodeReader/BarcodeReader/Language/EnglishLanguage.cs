using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Language
{
    public class EnglishLanguage : LanguageModel
    {
        public override string StartScreen_AddFile => "Create new document";

        public override string StartScreen_FtpSettings => "FTP settings";

        public override string StartScreen_LanguageSettings => "Language settings";

        public override string StartScreen_TableFilenameColumn => "Filename";

        public override string StartScreen_TableOperationColumn => "Operation";

        public override string StartScreen_DeleteFileInPopUp => "Are you sure you want to delete?";

        public override string DocumentList_AddGoods => "Add goods";

        public override string DocumentList_TableBarcodeColumnName => "Barcode";

        public override string DocumentList_TableAddedDateColumnName => "Added date";

        public override string DocumentList_TableCountName => "Count";

        public override string DocumentList_FileSave => "Save";

        public override string DocumentList_Cancel => "Cancel";

        public override string DocumentList_FileNameInputError => "The filename can only contain letters";

        public override string GeneralInformationPopUpHeader => "Information";

        public override string DocumentList_GeneralPopUpOk => "Ok";

        public override string GeneralPopUpConfirm => "Confirm";

        public override string GeneralPopUpCancel => "Cancel";

        public override string DocumentList_CancelPopUpMessage => "Data will not be saved!";

        public override string DocumentList_FileSavePopUpMessage => "You must specify a file name to save the file!";

        public override string DocumentList_ChangeCountInPopUpMessage => "Enter new count! Enter 0 to remove item!";

        public override string Recorder_KeepOpenPage => "Keep it open";

        public override string Recorder_ManualInputName => "Goods name";

        public override string Recorder_ManualInputButton => "Add";
    }
}
