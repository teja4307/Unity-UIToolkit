using UnityEngine;
using UnityEngine.UIElements;

public class LoadingScreenHandler : MonoBehaviour
{
   public static LoadingScreenHandler instance;

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
        progressBar = UiDocuments.instance.loadingScreenDoc.rootVisualElement.Q<ProgressBar>("ProgressBar");
        progressPercentage = UiDocuments.instance.loadingScreenDoc.rootVisualElement.Q<Label>("percentLabel");
    }
}
