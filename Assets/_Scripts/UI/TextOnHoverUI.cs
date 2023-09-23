using UnityEngine.EventSystems;

public abstract class TextOnHoverUI : TextOnHover, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.HoverInfoManager.SetSimpleText(Text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.HoverInfoManager.DeactiveHover();
    }
}
