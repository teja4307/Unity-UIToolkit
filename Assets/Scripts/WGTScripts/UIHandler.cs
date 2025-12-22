using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public UIDocument mainVisualTree;

    VisualElement gameSelection;
    VisualElement headToHead;

    //GameSelection
    Button headToHeadBtn;

    //Head-to-Head
    Button backButton;

    private void OnEnable()
    {
        gameSelection = mainVisualTree.rootVisualElement.Q<VisualElement>("GameSelection");
        headToHead = mainVisualTree.rootVisualElement.Q<VisualElement>("Head-to-HeadScreen");

        headToHeadBtn = gameSelection.Q<Button>("HeadtoHead");

        backButton = headToHead.Q<Button>("BackButton");

        headToHeadBtn.clicked += OpenHeadToHead;
        backButton.clicked += OpenGameSelection;

    }
    private void OnDisable()
    {
        headToHeadBtn.clicked -= OpenHeadToHead;
        backButton.clicked -= OpenGameSelection;
    }

    void OpenHeadToHead()
    {
        gameSelection.style.display = DisplayStyle.None;
        headToHead.style.display = DisplayStyle.Flex;
    }

    void OpenGameSelection()
    {
        gameSelection.style.display = DisplayStyle.Flex;
        headToHead.style.display = DisplayStyle.None;
    }
}
