using UnityEngine;
using UnityEngine.UIElements;

public class SecondSceneLoadingScreenHandler : MonoBehaviour
{
   public static SecondSceneLoadingScreenHandler instance;

    public Label progressPercentage;
    public ProgressBar progressBar;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        progressBar = SceneHandler.loadScreen.rootVisualElement.Q<ProgressBar>("ProgressBar");
        progressPercentage = SceneHandler.loadScreen.rootVisualElement.Q<Label>("percentLabel");
    }
}
