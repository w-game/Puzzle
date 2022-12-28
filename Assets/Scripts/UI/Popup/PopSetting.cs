using Ad;
using Common;
using TMPro;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PopSettingData : ViewData
{
    public override string ViewName => "PopSetting";
    public override ViewType ViewType => ViewType.Popup;
    public override bool Mask => true;
    public override bool AnimaSwitch => true;
}

public class PopSetting : PopupBase
{
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider soundEffect;
    [SerializeField] private Slider fpsSlider;
    [SerializeField] private Slider adSlider;
    [SerializeField] private SButton fpsBtn;
    [SerializeField] private SButton adBtn;
    [SerializeField] private SButton completeBtn;

    [Header("本地化")] 
    [SerializeField] private TextMeshProUGUI title;
    [Space]
    [SerializeField] private TextMeshProUGUI soundSection;
    [SerializeField] private TextMeshProUGUI bgmItemLabel;
    [SerializeField] private TextMeshProUGUI soundEffectItemLabel;
    [Space]
    [SerializeField] private TextMeshProUGUI gameSection;
    [SerializeField] private TextMeshProUGUI fpsItemLabel;
    [Space]
    [SerializeField] private TextMeshProUGUI adSection;
    [SerializeField] private TextMeshProUGUI popupAdItemLabel;

    public override void OnCreate(params object[] objects)
    {
        bgm.onValueChanged.AddListener(OnBgmValueChanged);
        soundEffect.onValueChanged.AddListener(OnSoundEffectValueChanged);
        fpsBtn.onClick.AddListener(OnFpsBtnClick);
        completeBtn.onClick.AddListener(CloseView);
        adBtn.onClick.AddListener(OnAdBtnClicked);
        
        Init();
    }

    public override void Localization()
    {
        var language = GameManager.Language;
        title.text = language.GameSettingTitle;
        
        completeBtn.SetButtonText(language.Done);
        
        soundSection.text = language.SoundText;
        soundEffectItemLabel.text = language.SoundEffectText;
        bgmItemLabel.text = language.BGMText;

        gameSection.text = language.GameText;
        fpsItemLabel.text = language.FPSText;

        adSection.text = language.AdText;
        popupAdItemLabel.text = language.PopupAdText;
    }

    private void Init()
    {
        fpsSlider.value = Application.targetFrameRate == 60 ? 1 : 0;
        bgm.value = SoundManager.Instance.BgmVolume;
        soundEffect.value = SoundManager.Instance.SoundEffectVolume;
        adSlider.value = AdManager.Instance.NativeAdSwitch ? 1 : 0;
    }

    private void OnBgmValueChanged(float value)
    {
        SoundManager.Instance.ChangeBgmVolume(value);
    }
    
    private void OnSoundEffectValueChanged(float value)
    {
        SoundManager.Instance.ChangeSoundEffectVolume(value);
    }

    private void OnFpsBtnClick()
    {
        fpsSlider.value = 1 - fpsSlider.value < 0.1f ? 0 : 1;
        Application.targetFrameRate = fpsSlider.value > 0.9f ? 60 : 30;
    }

    private void OnAdBtnClicked()
    {
        if (adSlider.value > 0.5f)
        {
            UIManager.Instance.ShowCheckBox(GameManager.Language.ClosePopupAdCheckDes, SetNativeSwitch);
        }
        else
        {
            SetNativeSwitch();
        }
    }

    private void SetNativeSwitch()
    {
        adSlider.value = adSlider.value > 0.5f ? 0 : 1;
        AdManager.Instance.NativeAdSwitch = adSlider.value > 0.5f;
    }
}