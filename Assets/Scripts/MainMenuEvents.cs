using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
   
    private Button _button;
    private Button settingsButton;
    private Button quitButton;
  
    private void Awake()
    {
        //document=GetComponent<UIDocument>();
        UiHandler.instance.Awake();
        _button = UiHandler.instance.document.rootVisualElement.Q<Button>("PlayButton");
        settingsButton = UiHandler.instance.document.rootVisualElement.Q<Button>("SettingsButton");
        quitButton = UiHandler.instance.document.rootVisualElement.Q<Button>("QuitButton");
       

    }
    
    private void OnEnable()
    {
        _button.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
       
    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
       


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
        UiHandler.instance.ActiveAndHide(UiHandler.instance.settingsDoc,UiHandler.instance.document);
        // Show Settings UI
        //settingsDoc.gameObject.SetActive(true);
    }
   
    void OnQuitButtonClick(ClickEvent _event)
    {
        Debug.Log("Quit Button Clicked");

        Application.Quit();
       // EditorApplication.Exit(1);
    } 

  
}
