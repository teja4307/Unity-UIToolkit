using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollCarouselHandler : MonoBehaviour
{

    private ScrollView scrollView;
    private Button leftBtn;
    private Button rightBtn;
    private Button backBtn;

    private Label[] indicators;
    private int index = 0;

    private void Start()
    {

        scrollView = UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<ScrollView>("CardScrollView");
        leftBtn = UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Button>("LeftButton");
        rightBtn = UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Button>("RightButton");
        backBtn = UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Button>("BackButton");

        indicators = new Label[]
        {
            UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Label>("indicator1"),
            UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Label>("indicator2"),
            UiDocuments.instance.scrollViewDoc.rootVisualElement.Q<Label>("indicator3")
        };

        leftBtn.clicked += MoveLeft;
        rightBtn.clicked += MoveRight;
        backBtn.clicked += OnBackButtonClick;
        UpdateIndicators();
    }
    private void OnDisable()
    {
        leftBtn.clicked -= MoveLeft;
        rightBtn.clicked-=MoveRight;
        backBtn.clicked -= OnBackButtonClick;
        UpdateIndicators();
    }

    private void MoveLeft()
    {
        if (index <= 0) return;
        index--;
        ScrollToIndex();
    }

    private void MoveRight()
    {
        if (index >= scrollView.childCount - 1) return;
        index++;
        ScrollToIndex();
    }

    private void ScrollToIndex()
    {
        scrollView.ScrollTo(scrollView[index]);
        UpdateIndicators();
    }

    private void UpdateIndicators()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].EnableInClassList("active", i == index);
        }
    }

    void OnBackButtonClick()
    {
        UiDocuments.instance.ActiveAndHide(UiDocuments.instance.document, 
                                           UiDocuments.instance.scrollViewDoc);
    }
}
