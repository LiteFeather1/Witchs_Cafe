using UnityEngine;

public abstract class TextOnHover : MonoBehaviour, IName
{
    protected abstract string Text { get; }
    public string Name => Text;

    private void OnMouseEnter() => GameManager.Instance.HoverInfoManager.SetSimpleText(Text);

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.DeactiveHover();
}
