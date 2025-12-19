using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollCarouselHandlerWGT : MonoBehaviour
{
    [Header("UI Document")]
    public UIDocument WGTScreen;

    private ScrollView scrollView;
    private VisualElement[] indicators;

    private int index = 0;
    private float cardWidth;
    private bool isSnapping;

    private void Start()
    {
        var root = WGTScreen.rootVisualElement;

        scrollView = root.Q<ScrollView>("CardScrollView");

        VisualElement indicatorsContainer = root.Q<VisualElement>("Indicators");

        indicators = indicatorsContainer
                        .Children()
                        .ToArray();

        // Wait until cards are added AND layout is resolved
        scrollView.schedule.Execute(Init).Until(() =>
            scrollView.contentContainer.childCount > 0 &&
            scrollView.resolvedStyle.width > 0 &&
            scrollView.contentContainer[0].resolvedStyle.width > 0
        );
    }

    private void Init()
    {
        // Each card fits the ScrollView width
        cardWidth = scrollView.resolvedStyle.width;

        // Listen to scroll movement
        scrollView.horizontalScroller.valueChanged += OnScrollChanged;

        UpdateIndicators();
    }

    private void OnDisable()
    {
        if (scrollView != null)
            scrollView.horizontalScroller.valueChanged -= OnScrollChanged;
    }

    private void OnScrollChanged(float value)
    {
        if (cardWidth <= 0 || isSnapping)
            return;

        int newIndex = Mathf.RoundToInt(value / cardWidth);
        newIndex = Mathf.Clamp(newIndex, 0, scrollView.contentContainer.childCount - 1);

        if (newIndex != index)
        {
            index = newIndex;
            UpdateIndicators();
        }

        // Snap after scroll settles
        scrollView.schedule.Execute(SnapToIndex).ExecuteLater(80);
    }

    private void SnapToIndex()
    {
        if (cardWidth <= 0)
            return;

        isSnapping = true;

        float target = index * cardWidth;
        scrollView.horizontalScroller.value = target;

        isSnapping = false;
    }

    private void UpdateIndicators()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].EnableInClassList("active", i == index);
        }
    }
}
