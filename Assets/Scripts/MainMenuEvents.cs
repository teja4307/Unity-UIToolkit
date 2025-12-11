using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private Button _button;
    private Button settingsButton;
    private Button quitButton;
    private Button scrollBar;
    private Button scrollView;
  
    private void Start()
    {
        _button = UiDocuments.instance.document.rootVisualElement.Q<Button>("PlayButton");
        settingsButton = UiDocuments.instance.document.rootVisualElement.Q<Button>("SettingsButton");
        quitButton = UiDocuments.instance.document.rootVisualElement.Q<Button>("QuitButton");
        scrollBar = UiDocuments.instance.document.rootVisualElement.Q<Button>("ScrollBar");
        scrollView = UiDocuments.instance.document.rootVisualElement.Q<Button>("ScrollView");
        _button.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        scrollBar.RegisterCallback<ClickEvent>(OnScrollBarButtonClick);
        scrollView.RegisterCallback<ClickEvent>(OnScrollViewButtonClick);
       
    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        scrollBar.UnregisterCallback<ClickEvent>(OnScrollBarButtonClick);
        scrollView.RegisterCallback<ClickEvent>(OnScrollViewButtonClick);


    }
    private void OnPlayButtonClick(ClickEvent _event)
    {
        Debug.Log("Start Button is Clicked");
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.loadingScreenDoc,UiDocuments.instance.document);
        LoadScene();
    }
    private void OnSettingsButtonClick(ClickEvent _event)
    {
        Debug.Log("Settings Button Clicked");

        // Hide Main Menu UI
        //document.gameObject.SetActive(false);
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.settingsDoc,UiDocuments.instance.document);
        // Show Settings UI
        //settingsDoc.gameObject.SetActive(true);
    }
   
    void OnQuitButtonClick(ClickEvent _event)
    {
        Debug.Log("Quit Button Clicked");

        Application.Quit();
       // EditorApplication.Exit(1);
    } 

  void OnScrollBarButtonClick(ClickEvent _event)
    {
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.scrollBarDoc,UiDocuments.instance.document);
    }
    void OnScrollViewButtonClick(ClickEvent _event)
    {
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.scrollViewDoc, UiDocuments.instance.document);
    }

    void LoadScene()
    {
        StartCoroutine(LoadSceneAsyc());
    }
    IEnumerator LoadSceneAsyc()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("New Scene");
        if(SceneManager.GetSceneByName("New Scene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("New Scene");
        }

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressvalue = operation.progress * 100;
            if (progressvalue >= 90)
            {
                progressvalue = 100f;
            }
            LoadingScreenHandler.instance.progressBar.value = progressvalue;
            LoadingScreenHandler.instance.progressPercentage.text = progressvalue.ToString() + "%";

            yield return new WaitForSeconds(0.5f);
            operation.allowSceneActivation=true;
            //yield return null;
        } 
    }
}
