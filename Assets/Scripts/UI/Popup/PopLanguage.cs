using Common;
using TMPro;
using UnityEngine;

namespace UI.Popup
{
    public class PopLanguageData : ViewData
    {
        public override string ViewName => "PopLanguage";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }
    public class PopLanguage : PopupBase
    {
        [SerializeField] private LanguageElement simplifiedChinese;
        [SerializeField] private LanguageElement traditionalChinese;
        [SerializeField] private LanguageElement english;
        [SerializeField] private SButton sureBtn;

        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI sureBtnTxt;
        public override void OnCreate(params object[] objects)
        {
            simplifiedChinese.Init(ELanguage.SimplifiedChinese, () =>
            {
                traditionalChinese.UnSelect();
                english.UnSelect();
            });
            
            traditionalChinese.Init(ELanguage.TraditionalChinese, () =>
            {
                simplifiedChinese.UnSelect();
                english.UnSelect();
            });
            
            english.Init(ELanguage.English, () =>
            {
                simplifiedChinese.UnSelect();
                traditionalChinese.UnSelect();
            });

            if (GameManager.LanguageType == simplifiedChinese.Type)
            {
                simplifiedChinese.Select();
            }
            
            if (GameManager.LanguageType == traditionalChinese.Type)
            {
                traditionalChinese.Select();
            }
            
            if (GameManager.LanguageType == english.Type)
            {
                english.Select();
            }
            
            sureBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.GetHomeView().Localization();
                CloseView();
            });
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            simplifiedChinese.LanguageName = language.SimplifiedChineseText;
            traditionalChinese.LanguageName = language.TraditionalChineseText;
            english.LanguageName = language.EnglishText;

            title.text = language.LanguageTitle;
            sureBtnTxt.text = language.SureText;
        }
    }
}