using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor;
    public Color hoverColor = new Color(0.25f, 0.61f, 0.80f); // Change this to the color you want when hovered

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        normalColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor;
    }
}
