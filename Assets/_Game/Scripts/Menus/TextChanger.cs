using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;

    void Start() {
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (buttonText != null)
        {
            buttonText.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }
}
