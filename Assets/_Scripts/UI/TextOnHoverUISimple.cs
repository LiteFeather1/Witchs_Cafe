using UnityEngine;

public class TextOnHoverUISimple : TextOnHoverUI
{
    [SerializeField] private string _info;

    protected override string Text => _info;
}
