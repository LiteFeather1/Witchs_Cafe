public class TextOnHoverGiveIngredient : TextOnHoverSimple
{
    private IGiveDraggable _giveDraggable;

    private void Awake() => _giveDraggable = GetComponent<IGiveDraggable>();

    public void OnEnable() => _giveDraggable.OnGrabbed += base.OnMouseExit;

    private void OnDisable() => _giveDraggable.OnGrabbed -= base.OnMouseExit;

    protected override void OnMouseEnter()
    {
        if (GameManager.Instance.MouseManager.Draggable == null)
            base.OnMouseEnter();
    }
    protected override void OnMouseExit()
    {
        if (GameManager.Instance.MouseManager.Draggable == null)
            base.OnMouseExit();
    }
}
