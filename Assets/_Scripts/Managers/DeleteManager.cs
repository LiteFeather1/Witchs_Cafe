using UnityEngine;
using UnityEngine.InputSystem;

public class DeleteManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMasks;

    private void OnEnable()
    {
        GameManager.InputManager.PlayerInputs.RightClick.performed += DeleteIngredient;
    }

    private void OnDisable()
    {
        GameManager.InputManager.PlayerInputs.RightClick.performed -= DeleteIngredient;
    }

    private void DeleteIngredient(InputAction.CallbackContext ctx)
    {
        Vector2 mousePoint = GameManager.MousePosition();
        var collider = Physics2D.OverlapPoint(mousePoint, _layerMasks);
        if (collider == null)
            return;

        if (collider.TryGetComponent(out IDestroyable destroyable))
            destroyable.Destroy();
    }
}
