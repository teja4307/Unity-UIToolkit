using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private UIDocument settingsDoc;
    private Button _button;
    private Button settingsButton;
    private Button backButton;

    private void Awake()
    {
        //document=GetComponent<UIDocument>();

        _button = document.rootVisualElement.Q<Button>("PlayButton");
        settingsButton = document.rootVisualElement.Q<Button>("SettingsButton");
        backButton = settingsDoc.rootVisualElement.Q<Button>("BackButton");
        settingsDoc.rootVisualElement.style.display = DisplayStyle.None;

    }
    
    private void OnEnable()
    {
        _button.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClick);


    }
    private void OnPlayButtonClick(ClickEvent _event)
    {
        Debug.Log("Start Button is Clicked");
    }
    private void OnSettingsButtonClick(ClickEvent _event)
    {
        Debug.Log("Settings Button Clicked");

        // Hide Main Menu UI
        //document.gameObject.SetActive(false);
        document.rootVisualElement.style.display = DisplayStyle.None;
        // Show Settings UI
        //settingsDoc.gameObject.SetActive(true);
        settingsDoc.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    private void OnBackButtonClick(ClickEvent _event)
    {
        Debug.Log("Back Button Clicked");

        // Hide Main Menu UI
        //document.gameObject.SetActive(true);
        document.rootVisualElement.style.display = DisplayStyle.Flex;

        // Show Settings UI
        //settingsDoc.gameObject.SetActive(false);
        settingsDoc.rootVisualElement.style.display = DisplayStyle.None;

    }
}
