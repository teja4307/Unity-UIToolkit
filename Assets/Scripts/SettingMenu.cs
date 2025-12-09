using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private Slider masterSlider;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Button backButton;


    private void Awake()
    {
        UiHandler.instance.Awake();
        masterSlider = UiHandler.instance.settingsDoc.rootVisualElement.Q<Slider>("MasterSlider");
        musicSlider = UiHandler.instance.settingsDoc.rootVisualElement.Q<Slider>("MusicSlider");
        sfxSlider = UiHandler.instance.settingsDoc.rootVisualElement.Q<Slider>("SFXSlider");
        backButton = UiHandler.instance.settingsDoc.rootVisualElement.Q<Button>("BackButton");
    }

    private void OnEnable()
    {
        masterSlider.RegisterValueChangedCallback(evt => SetMasterVolume(evt.newValue));
        musicSlider.RegisterValueChangedCallback(evt => SetMusicVolume(evt.newValue));
        sfxSlider.RegisterValueChangedCallback(evt => SetSFXVolume(evt.newValue));
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);

    }
    private void OnDisable()
    {
        masterSlider.UnregisterValueChangedCallback(evt => SetMasterVolume(evt.newValue));
        musicSlider.UnregisterValueChangedCallback(evt => SetMusicVolume(evt.newValue));
        sfxSlider.UnregisterValueChangedCallback(evt => SetSFXVolume(evt.newValue));
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClick);

    }

    void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", SliderToDecibals(volume));
    }
    void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", SliderToDecibals(volume));
    }
    void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", SliderToDecibals(volume));
    }

    float SliderToDecibals(float value)
    {
        if (value <= 0.001)
            return -80f;

        return Mathf.Log10(value) * 20f;
    }
    private void OnBackButtonClick(ClickEvent _event)
    {
        Debug.Log("Back Button Clicked");

        // Hide Main Menu UI
        //document.gameObject.SetActive(true);
        UiHandler.instance.ActiveAndHide(UiHandler.instance.document,UiHandler.instance.settingsDoc);
        // Show Settings UI
        //settingsDoc.gameObject.SetActive(false);

    }
}
