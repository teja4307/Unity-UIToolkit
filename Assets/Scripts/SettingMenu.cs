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
    private void Start()
    {
        masterSlider = UiDocuments.instance.settingsDoc.rootVisualElement.Q<Slider>("MasterSlider");
        musicSlider = UiDocuments.instance.settingsDoc.rootVisualElement.Q<Slider>("MusicSlider");
        sfxSlider = UiDocuments.instance.settingsDoc.rootVisualElement.Q<Slider>("SFXSlider");
        backButton = UiDocuments.instance.settingsDoc.rootVisualElement.Q<Button>("BackButton");
        masterSlider.RegisterValueChangedCallback(evt => SetMasterVolume(evt.newValue));
        musicSlider.RegisterValueChangedCallback(evt => SetMusicVolume(evt.newValue));
        sfxSlider.RegisterValueChangedCallback(evt => SetSFXVolume(evt.newValue));
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

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
        PlayerPrefs.SetFloat("MasterVolume",masterSlider.value);
    }
    void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", SliderToDecibals(volume));
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
    void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", SliderToDecibals(volume));
        PlayerPrefs.SetFloat("SFXVolume",sfxSlider.value);
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
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.document,UiDocuments.instance.settingsDoc);
        // Show Settings UI
        //settingsDoc.gameObject.SetActive(false);

    }
}
