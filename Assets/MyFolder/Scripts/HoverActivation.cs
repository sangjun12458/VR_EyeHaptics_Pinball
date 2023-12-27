using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverActivation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private enum UIElement
    {
        Button,
        Toggle,
        Slider
    }

    [SerializeField]
    private UIElement _element;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스를 가져다대면 이 버튼을 활성화합니다.
        switch (_element)
        {
            case UIElement.Button:
                GetComponent<Button>().interactable = true;
                break;
            case UIElement.Toggle:
                GetComponent<Toggle>().interactable = true;
                break;
            case UIElement.Slider:
                GetComponent<Slider>().interactable = true;
                break;
            default:
                break;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 버튼에서 벗어나면 이 버튼을 다시 비활성화합니다.
        switch (_element)
        {
            case UIElement.Button:
                GetComponent<Button>().interactable = false;
                break;
            case UIElement.Toggle:
                GetComponent<Toggle>().interactable = false;
                break;
            case UIElement.Slider:
                GetComponent<Slider>().interactable = false;
                break;
            default:
                break;
        }
    }
}
