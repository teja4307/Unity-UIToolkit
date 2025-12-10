using UnityEngine;
using UnityEngine.UIElements;

public class ScrollBarMenu : MonoBehaviour
{

    private Button backButton;


    void Start()
    {
        backButton = UiDocuments.instance.scrollBarDoc.rootVisualElement.Q<Button>("BackButton");

        backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
    }
    private void OnDisable()
    {
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClick);

    }

    void OnBackButtonClick(ClickEvent evt)
    {
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.document, UiDocuments.instance.scrollBarDoc);
    }
}
