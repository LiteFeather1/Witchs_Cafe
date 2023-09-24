public class TextOnHoverGiveIngredient : TextOnHoverSimple
{
    private IGiveDraggable _giveDraggable;

    private void Awake() => _giveDraggable = GetComponent<IGiveDraggable>();

    public void OnEnable() => _giveDraggable.OnGrabbed += OnMouseExit;

    private void OnDisable() => _giveDraggable.OnGrabbed -= OnMouseExit;

    protected override void OnMouseEnter()
    {
        if (GameManager.Instance.MouseManager.Draggable == null)
            base.OnMouseEnter();
    }
}
