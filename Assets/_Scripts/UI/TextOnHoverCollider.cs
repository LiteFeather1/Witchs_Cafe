public abstract class TextOnHoverCollider : TextOnHover, IName
{
    public string Name => Text;

    private void OnMouseEnter() => GameManager.Instance.HoverInfoManager.SetSimpleText(Text);

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.DeactiveHover();
}
