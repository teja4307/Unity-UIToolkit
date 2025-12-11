using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocuments;
    public static UIDocument loadScreen;

    private Button mainMenu;

    private void Awake()
    {
        loadScreen=GameObject.Find("LoadDocument").GetComponent<UIDocument>();

        uiDocuments.rootVisualElement.style.display = DisplayStyle.Flex;
        loadScreen.rootVisualElement.style.display = DisplayStyle.None;

        mainMenu = uiDocuments.rootVisualElement.Q<Button>("MenuButton");
    }

    private void OnEnable()
    {
        mainMenu.clicked += OnMenuButtonClick;
    }

    void OnMenuButtonClick()
    {
        uiDocuments.rootVisualElement.style.display = DisplayStyle.None;
        loadScreen.rootVisualElement.style.display = DisplayStyle.Flex;
        StartCoroutine(LoadSceneAsyc());

    }
    IEnumerator LoadSceneAsyc()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
        if (SceneManager.GetSceneByName("SampleScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("SampleScene");
        }
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressvalue = operation.progress * 100;
            if (progressvalue >= 90)
            {
                progressvalue = 100f;
            }
            SecondSceneLoadingScreenHandler.instance.progressBar.value = progressvalue;
            SecondSceneLoadingScreenHandler.instance.progressPercentage.text = progressvalue.ToString() + "%";

            yield return new WaitForSeconds(0.5f);
            operation.allowSceneActivation = true;
            //yield return null;
        }
    }
}
