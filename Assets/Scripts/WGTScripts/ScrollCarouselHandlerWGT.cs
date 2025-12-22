using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollCarouselHandlerWGT : MonoBehaviour
{
    [Header("UI Document")]
    public UIDocument WGTScreen;

    private ScrollView scrollView;
    private VisualElement viewport;
    private VisualElement[] indicators;

    private int index = 0;
    private float cardWidth;

    // Drag state
    private bool isDragging;
    private float startPointerX;
    private float startScrollX;

    private void Start()
    {
        var root = WGTScreen.rootVisualElement;

        // Get ScrollView
        scrollView = root.Q<ScrollView>("CardScrollView");
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;

        // 🔑 Get viewport (THIS is the interactive element)
        viewport = scrollView.Q("unity-content-viewport");
        viewport.pickingMode = PickingMode.Position;

        // Register pointer callbacks
        viewport.RegisterCallback<PointerDownEvent>(OnPointerDown);
        viewport.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        viewport.RegisterCallback<PointerUpEvent>(OnPointerUp);
        viewport.RegisterCallback<PointerCancelEvent>(OnPointerUp);

        // Indicators
        VisualElement indicatorsContainer = root.Q<VisualElement>("Indicators");
        indicators = indicatorsContainer.Children().ToArray();

        // Wait for layout
        scrollView.schedule.Execute(Init).Until(() =>
            scrollView.contentContainer.childCount > 0 &&
            scrollView.resolvedStyle.width > 0 &&
            scrollView.contentContainer[0].resolvedStyle.width > 0
        );
    }

    private void Init()
    {
        cardWidth = scrollView.resolvedStyle.width;

        // Listen to wheel / scrollbar scroll
        scrollView.horizontalScroller.valueChanged += OnScrollChanged;

        UpdateIndicators();
    }

    private void OnDisable()
    {
        if (scrollView != null)
            scrollView.horizontalScroller.valueChanged -= OnScrollChanged;
    }

    // ================= POINTER DRAG =================

    private void OnPointerDown(PointerDownEvent evt)
    {
        isDragging = true;
        startPointerX = evt.position.x;
        startScrollX = scrollView.scrollOffset.x;

        viewport.CapturePointer(evt.pointerId);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!isDragging)
            return;

        float delta = evt.position.x - startPointerX;
        scrollView.scrollOffset = new Vector2(startScrollX - delta, 0);
    }

    private void OnPointerUp(EventBase evt)
    {
        if (!isDragging)
            return;

        isDragging = false;
        viewport.ReleasePointer(PointerId.mousePointerId);

        SnapToNearest();
    }

    // ================= SCROLL LOGIC =================

    private void OnScrollChanged(float value)
    {
        if (cardWidth <= 0)
            return;

        int newIndex = Mathf.RoundToInt(value / cardWidth);
        newIndex = Mathf.Clamp(newIndex, 0, scrollView.contentContainer.childCount - 1);

        if (newIndex != index)
        {
            index = newIndex;
            UpdateIndicators();
        }
    }

    private void SnapToNearest()
    {
        index = Mathf.RoundToInt(scrollView.scrollOffset.x / cardWidth);
        index = Mathf.Clamp(index, 0, scrollView.contentContainer.childCount - 1);

        scrollView.scrollOffset = new Vector2(index * cardWidth, 0);
        UpdateIndicators();
    }

    private void UpdateIndicators()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].EnableInClassList("active", i == index);
        }
    }
}
