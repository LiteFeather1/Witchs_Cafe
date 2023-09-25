public abstract class TextOnHoverCollider : TextOnHover, IName
{
    public string Name => Text;

    protected virtual void OnMouseEnter()
    {
        GameManager.Instance.HoverInfoManager.SetSimpleText(Text);
    }

    protected virtual void OnMouseExit()
    {
        GameManager.Instance.HoverInfoManager.HideHover();
    }
}
