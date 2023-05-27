using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Language
{
    public abstract class LanguageModel
    {

        public static LanguageModel GenerateLanguage(Languages language)
        {
            switch (language)
            {
                case Languages.Azerbaijan:
                    {
                        LanguageModel model = new AzerbaijanLanguage();
                        return model;
                    }
                case Languages.Turkish:
                    {
                        LanguageModel model = new TurkishLanguage();
                        return model;
                    }
                case Languages.English:
                    {
                        LanguageModel model = new EnglishLanguage();
                        return model;
                    }
                default:
                    {
                        throw new Exception("Invalid Language Code");
                    }
            }
        }

        public static string[] GetLangueages()
        {
            return new string[]
            {
                "English",
                "Azərbaycanca",
                "Türkçe"
            };
        }
        public static Languages StringToLanguage(string language)
        {
            switch (language)
            {
                case "English":
                    {
                        return Languages.English;
                    }

                case "Azərbaycanca":
                    {
                        return Languages.Azerbaijan;
                    }

                case "Türkçe":
                    {
                        return Languages.Turkish;
                    }
                default:
                    {
                        throw new Exception("Invalid text format");
                    }
            }
        }

        public abstract string StartScreen_AddFile { get; }
        public abstract string StartScreen_FtpSettings { get; }
        public abstract string StartScreen_LanguageSettings { get; }
        public abstract string StartScreen_TableFilenameColumn { get; }
        public abstract string StartScreen_TableOperationColumn { get; }
        public abstract string StartScreen_DeleteFileInPopUp { get; }
        public abstract string DocumentList_AddGoods { get; }
        public abstract string DocumentList_TableBarcodeColumnName { get; }
        public abstract string DocumentList_TableAddedDateColumnName { get; }
        public abstract string DocumentList_TableCountName { get; }
        public abstract string DocumentList_FileSave { get; }
        public abstract string DocumentList_Cancel { get; }
        public abstract string DocumentList_FileNameInputError { get; }
        public abstract string GeneralInformationPopUpHeader { get; }
        public abstract string DocumentList_GeneralPopUpOk { get; }
        public abstract string GeneralPopUpConfirm { get; }
        public abstract string GeneralPopUpCancel { get; }
        public abstract string DocumentList_CancelPopUpMessage { get; }
        public abstract string DocumentList_FileSavePopUpMessage { get; }
        public abstract string DocumentList_ChangeCountInPopUpMessage { get; }
        public abstract string Recorder_KeepOpenPage { get; }
        public abstract string Recorder_ManualInputName { get; }
        public abstract string Recorder_ManualInputButton { get; }
    }
}
