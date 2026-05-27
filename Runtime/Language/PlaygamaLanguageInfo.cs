using PrimeGames.SDK.Common;
using Playgama;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(ILanguageInfo))]
    public class PlaygamaLanguageInfo : CommonLanguageInfo {

        public PlaygamaLanguageInfo() : base() {
            SetInitialized();
        }

        protected override LanguageType GetCurrentImpl() {
            return Bridge.platform.language switch {
                "en" => LanguageType.English,
                "ru" => LanguageType.Russian,
                "ja" => LanguageType.Japanese,
                "zh" => LanguageType.Chinese,
                "tr" => LanguageType.Turkish,
                "hi" => LanguageType.Hindi,
                "ko" => LanguageType.Korean,
                "pt" => LanguageType.Portuguese,
                "id" => LanguageType.Indonesian,
                "de" => LanguageType.German,
                "es" => LanguageType.Spanish,
                "it" => LanguageType.Italian,
                "uk" => LanguageType.Ukrainian,
                "pl" => LanguageType.Polish,
                "fr" => LanguageType.French,
                "da" => LanguageType.Danish,
                "cs" => LanguageType.Czech,
                "af" => LanguageType.Afrikaans,
                "is" => LanguageType.Icelandic,
                "no" => LanguageType.Norwegian,
                "sv" => LanguageType.Swedish,
                "nl" => LanguageType.Dutch,
                "sk" => LanguageType.Slovak,
                "th" => LanguageType.Thai,
                "vi" => LanguageType.Vietnamese,
                _ => LanguageType.English,
            };
        }

    }

}