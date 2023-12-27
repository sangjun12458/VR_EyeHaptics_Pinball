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
        // ���콺�� �����ٴ�� �� ��ư�� Ȱ��ȭ�մϴ�.
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
        // ���콺�� ��ư���� ����� �� ��ư�� �ٽ� ��Ȱ��ȭ�մϴ�.
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
