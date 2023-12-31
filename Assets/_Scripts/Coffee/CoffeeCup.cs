﻿using System.Xml;
using UnityEngine;

public class CoffeeCup : ReceiveIngredient<ITopping>
{
    [SerializeField] private Coffee _deliverCoffee;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Draggable _coffeeCupDraggable;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer[] _srs;
    [SerializeField] private Material _outlineMat;
    [SerializeField] private Material _unoutlineMat;

    [Header("Points")]
    [SerializeField] private Transform _storePoint;
    [SerializeField] private Transform _kitchenPoint;

    [Header("Hover")]
    [SerializeField] private TranslatedString _name;

    [Header("Sounds")]
    [SerializeField] private AudioClip _audioCoffeeTransferred;

    public Coffee DeliverCoffee => _deliverCoffee;

    public bool CanReceiveCoffee => _deliverCoffee.CoffeeBean == null;

    private void OnMouseEnter() => SetHoverText();

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.HideHover();

    private void SetHoverText()
    {
        if (_t != null)
        {
            switch (GameManager.Instance.Language)
            {
                case Languages.Portuguese:
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Adicionar {_t.Name.PT_String} no {_name.PT_String}?");
                    break;
                default:
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Add {_t.Name.EN_String} to {_name.EN_String}?");
                    break;
            }
        }
        else
        {
            var mouseDraggable = GameManager.Instance.MouseManager.Draggable;
            if (mouseDraggable != null)
            {
                if (mouseDraggable.RB.TryGetComponent(out IName nameable))
                {
                    switch (GameManager.Instance.Language)
                    {
                        case Languages.Portuguese:
                            GameManager.Instance.HoverInfoManager.SetSimpleText($"Não posso {nameable.Name.PT_String} no {_name.PT_String}!!!");
                            break;
                        default:
                            GameManager.Instance.HoverInfoManager.SetSimpleText($"Can't add {nameable.Name.EN_String} to {_name.EN_String}!!!");
                            break;
                    }
                }
            }
            else if (_deliverCoffee.CoffeeBean == null
                && _deliverCoffee.Milk == null
                && _deliverCoffee.MiscIngredients.Count == 0)
            {
                switch (GameManager.Instance.Language)
                {
                    case Languages.Portuguese:
                        GameManager.Instance.HoverInfoManager.SetSimpleText($"{_name.PT_String} vazio!");
                        break;
                    default:
                        GameManager.Instance.HoverInfoManager.SetSimpleText($"{_name.EN_String} is empty!");
                        break;
                }
            }
            else
                GameManager.Instance.HoverInfoManager.SetCoffeeText(_name.String(GameManager.Instance.Language), _deliverCoffee);
        }
    }

    public void ReceiveCoffee(Coffee from)
    {
        _deliverCoffee = from;
        GameManager.Instance.AudioManager.PlaySFX(_audioCoffeeTransferred);
    }

    // Also Called by a unity event
    public void TrashCoffee() => _deliverCoffee = new();

    // Also Called by a unity event
    public void TeleportToStore()
    {
        transform.position = _storePoint.position;
        _coffeeCupDraggable.enabled = true;
        _coffeeCupDraggable.RB.bodyType = RigidbodyType2D.Dynamic;
        _coffeeCupDraggable.Collider.isTrigger = false;
        _coffeeCupDraggable.IsHold = false;
        for (int i = 0; i < _srs.Length; i++)
            _srs[i].material = _outlineMat;
    }

    // Also Called by a unity event
    public void TeleportToKitchen()
    {
        transform.position = _kitchenPoint.position;
        _coffeeCupDraggable.enabled = false;
        _coffeeCupDraggable.RB.bodyType = RigidbodyType2D.Static;
        _coffeeCupDraggable.Collider.isTrigger = true;
        _coffeeCupDraggable.IsHold = false;
        for (int i = 0; i < _srs.Length; i++)
            _srs[i].material = _unoutlineMat;
        TrashCoffee();
    }

    protected override void TakeIngredient()
    {
        _t.AddIngredient(_deliverCoffee);
        var t = _t;
        base.TakeIngredient();
        t.Destroy();

        if (_collider.bounds.Contains(GameManager.MousePosition()))
            SetHoverText();
    }
}
