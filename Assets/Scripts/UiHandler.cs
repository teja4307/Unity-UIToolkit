using UnityEngine;
using UnityEngine.UIElements;

public class UiHandler : MonoBehaviour
{
    public static UiHandler instance;

    public UIDocument document;
    public UIDocument settingsDoc;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        settingsDoc.rootVisualElement.style.display=DisplayStyle.None;
    }


    public void ActiveAndHide(UIDocument active,UIDocument hide)
    {
        hide.rootVisualElement.style.display = DisplayStyle.None;
        active.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
