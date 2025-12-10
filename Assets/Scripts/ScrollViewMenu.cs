using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ScrollViewMenu : MonoBehaviour
{

    private Button backButton;


    void Start()
    {
        backButton = UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Button>("BackButton");

        backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
    }
    private void OnDisable()
    {
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClick);

    }

    void OnBackButtonClick(ClickEvent evt)
    {
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.document, UiDocuments.instance.scrollViewDoc);
    }
}
