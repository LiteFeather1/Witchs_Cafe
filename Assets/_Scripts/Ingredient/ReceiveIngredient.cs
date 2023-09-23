using UnityEngine;

public abstract class ReceiveIngredient<T> : MonoBehaviour where T : class
{
    protected T _t;
    protected IDraggable _draggable = null;
        
    protected virtual void TakeIngredient()
    {
        _draggable.Hold();
        ClearReferences();
    }

    protected virtual void ClearReferences()
    {
        _draggable.Released -= TakeIngredient;
        _draggable = null;
        _t = null;
    }

    protected virtual void OnFound(T t)
    {
        _t = t;
        if (_draggable.IsDragging)
            _draggable.Released += TakeIngredient;
        else
            TakeIngredient();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
#pragma warning disable UNT0014 // T is not a unity componet but kinda is xd
        if (!collision.TryGetComponent(out T t))
            return;
#pragma warning restore UNT0014

        _draggable = collision.GetComponent<IDraggable>();
        OnFound(t);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
#pragma warning disable UNT0014 // T is not a unity componet but kinda is xd
        if (!collision.TryGetComponent(out T _))
            return;
#pragma warning restore UNT0014

        if (_draggable == null)
            return;

        ClearReferences();
    }
}
