using UnityEngine;

public class Laddle : MonoBehaviour
{
    [SerializeField] private Cauldron _cauldron;
    [SerializeField] private ReturnToStartPosAfterRelease _returnToStartPosAfterRelease;
    private IDraggable _draggable;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer _waterSprite;

    private void Awake() => _draggable = GetComponent<Draggable>();

    private void OnEnable() => _cauldron.OnCoffeChange += SetWaterColour;

    private void OnDisable() => _cauldron.OnCoffeChange -= SetWaterColour;

    private void SetWaterColour(Color colour) => _waterSprite.color = colour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_cauldron.MixingCoffee.CoffeeBean == null)
            return;

        if (!collision.TryGetComponent(out CoffeeCup coffeeCup))
            return;

        if (!coffeeCup.ReceiveCoffee(_cauldron.MixingCoffee))
            return;

        _cauldron.TrashCoffee();
        _draggable.ForceRelease();
        _returnToStartPosAfterRelease.Teleport();
    }
}