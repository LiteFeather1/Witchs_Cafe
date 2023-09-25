using UnityEngine;

public class Laddle : MonoBehaviour
{
    [SerializeField] private Cauldron _cauldron;
    private CoffeeCup _coffeeCup;
    [SerializeField] private ReturnToStartPosAfterRelease _returnToStartPosAfterRelease;
    private IDraggable _draggable;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer _waterSprite;

    private void Awake() => _draggable = GetComponent<Draggable>();

    private void OnEnable()
    {
        _cauldron.OnCoffeChange += SetWaterColour;
        _draggable.OnReleased += DropCoffee;
    }

    private void OnDisable()
    {
        _cauldron.OnCoffeChange -= SetWaterColour;
        _draggable.OnReleased -= DropCoffee;
    }

    private void SetWaterColour(Color colour) => _waterSprite.color = colour;

    private void DropCoffee()
    {
        if (_coffeeCup == null || !_coffeeCup.CanReceiveCoffee)
            return;

        _coffeeCup.ReceiveCoffee(_cauldron.MixingCoffee);
        _cauldron.TrashCoffee();
        _draggable.ForceRelease();
        _returnToStartPosAfterRelease.Teleport();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out CoffeeCup coffeeCup))
            return;

        HoverInfoManager hoverInfoManager = GameManager.Instance.HoverInfoManager;
        if (_cauldron.MixingCoffee.CoffeeBean == null)
        {
            hoverInfoManager.SetSimpleText("No Coffee to add!");
            return;
        }

        var info = coffeeCup.CanReceiveCoffee ? "Add Coffee into Cup" : "Cup Already has coffee!";
        hoverInfoManager.SetSimpleText(info);

        _coffeeCup = coffeeCup;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out CoffeeCup _))
            return;

        GameManager.Instance.HoverInfoManager.HideHover();
        _coffeeCup = null;
    }
}