using UnityEngine;
using UnityEngine.UIElements;

public class UiDocuments : MonoBehaviour
{
    public static UiDocuments instance;

    public UIDocument document;
    public UIDocument settingsDoc;
    public UIDocument scrollBarDoc;
    public UIDocument scrollViewDoc;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        settingsDoc.rootVisualElement.style.display=DisplayStyle.None;
        scrollBarDoc.rootVisualElement.style.display =DisplayStyle.None;
        scrollViewDoc.rootVisualElement.style.display =DisplayStyle.None;
    }


    public void ActiveAndHide(UIDocument active,UIDocument hide)
    {
        hide.rootVisualElement.style.display = DisplayStyle.None;
        active.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
